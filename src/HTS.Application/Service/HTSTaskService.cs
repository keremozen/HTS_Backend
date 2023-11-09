using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Common;
using HTS.Data.Entity;
using HTS.Dto.HTSTask;
using HTS.Enum;
using HTS.Interface;
using HTS.Localization;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace HTS.Service;
public class HTSTaskService : ApplicationService, IHTSTaskService
{
    private readonly IRepository<HTSTask, int> _taskRepository;
    private readonly IRepository<HospitalPricer, int> _hospitalPricerRepository;
    private readonly IRepository<Patient, int> _patientRepository;
    private IRepository<Operation, int> _operationRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;
    private readonly IStringLocalizer<HTSResource> _localizer;
    public HTSTaskService(IRepository<HTSTask, int> taskRepository,
        IRepository<HospitalPricer, int> hospitalPricerRepository,
        IRepository<Patient, int> patientRepository,
        IRepository<Operation, int> operationRepository,
        ICurrentUser currentUser,
        IUserService userService,
        IStringLocalizer<HTSResource> localizer)
    {
        _taskRepository = taskRepository;
        _currentUser = currentUser;
        _hospitalPricerRepository = hospitalPricerRepository;
        _patientRepository = patientRepository;
        _operationRepository = operationRepository;
        _userService = userService;
        _localizer = localizer;
    }

    public async Task<PagedResultDto<HTSTaskDto>> GetListAsync()
    {
        //Get all entities
        var query = (await _taskRepository.WithDetailsAsync())
            .Where(t => t.UserId == _currentUser.Id && t.IsActive);
        var responseList = ObjectMapper.Map<List<HTSTask>, List<HTSTaskDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _taskRepository.CountAsync();//item count
        return new PagedResultDto<HTSTaskDto>(totalCount, responseList);
    }

    public async Task AssignToTik(int userId)
    {
        var patient = await _patientRepository.FirstOrDefaultAsync(p => p.Id == userId);
        IsDataValidToAssignToTik(patient);
        //Get tik users
        var tikUsers = await _userService.GetTikStaffListAsync();
        if (!(tikUsers?.Any() ?? false))
        {
            throw new HTSBusinessException(ErrorCode.NoAssignedUserToTickRole);
        }

        //Create task
        List<HTSTask> tasks = new List<HTSTask>();
        foreach (var tik in tikUsers)
        {
            tasks.Add(new HTSTask()
            {
                TaskTypeId = EntityEnum.TaskTypeEnum.Tik.GetHashCode(),
                UserId = tik.Id,
                IsActive = true,
                PatientId = patient.Id,
                Url = "https://webhts.ushas.com.tr/patient/edit/" + patient.Id
            });
        }
        await _taskRepository.InsertManyAsync(tasks);
        List<string> toList = tikUsers.Select(p => p.Email).ToList();
        //Send email
        SendEMailToTikUsers(toList, patient);
        //Update patient
        patient.IsAssignedToTik = true;
        patient.TikAssignmentDate = DateTime.Now;
        patient.UserIdAssignedToTik = _currentUser.Id;
        await _patientRepository.UpdateAsync(patient);
    }

    public async Task ReturnFromTik(int userId)
    {
        var patient = await _patientRepository.FirstOrDefaultAsync(p => p.Id == userId);
        IsDataValidToReturnFromTik(patient);

        //Close tasks
        var tasks = (await _taskRepository.GetQueryableAsync()).Where(t =>
            t.PatientId == userId && t.TaskTypeId == EntityEnum.TaskTypeEnum.Tik.GetHashCode()).ToList();
        if (tasks?.Any() ?? false)
        {
            tasks = tasks.Select(t =>
            {
                t.IsActive = false;
                return t;
            }).ToList();
            await _taskRepository.UpdateManyAsync(tasks);
        }

        //Update patient
        patient.IsAssignedToTik = false;
        patient.TikReturnDate = DateTime.Now;
        patient.TikUserIdReturned = _currentUser.Id;
        await _patientRepository.UpdateAsync(patient);
    }


    public async Task CreateAsync(SaveHTSTaskDto saveTask)
    {
        if (saveTask.TaskType == EntityEnum.TaskTypeEnum.Pricing)
        {
            await DoPricingTaskProcess(saveTask);
        }
        else if (saveTask.TaskType == EntityEnum.TaskTypeEnum.PatientApproval)
        {
            await DoPatientApprovalProcess(saveTask);
        }


    }

    private async Task DoPricingTaskProcess(SaveHTSTaskDto saveTask)
    {
        if (saveTask.HospitalId.HasValue)
        {
            //Get pricers
            var pricers = (await _hospitalPricerRepository.WithDetailsAsync((p => p.User)))
                .Where(p => p.HospitalId == saveTask.HospitalId).ToList();
            if (!(pricers?.Any() ?? false))
            {//No pricer
                throw new HTSBusinessException(ErrorCode.NoUserInHospitalPricer);
            }
            //Create Task
            List<HTSTask> tasks = new List<HTSTask>();
            foreach (var pricer in pricers)
            {
                tasks.Add(new HTSTask()
                {
                    TaskTypeId = EntityEnum.TaskTypeEnum.Pricing.GetHashCode(),
                    UserId = pricer.UserId,
                    IsActive = true,
                    PatientId = saveTask.PatientId,
                    RelatedEntityId = saveTask.RelatedEntityId,
                    Url = "https://webhts.ushas.com.tr/patient/edit/" + saveTask.PatientId
                });
            }
            await _taskRepository.InsertManyAsync(tasks);
            //Send Email
            List<string> toList = pricers.Select(p => p.User.Email).ToList();
            await SendEMailToPricerUsers(toList, saveTask);
        }
    }

    private async Task DoPatientApprovalProcess(SaveHTSTaskDto saveTask)
    {

        //Operasyon kaydını oluşturan kullanıcıya, ya da Tik e devredilmiş ise tik kullanıcılarına
        List<Guid> taskUsers = new List<Guid>();
        List<string> toList = new List<string>();
        var patient = await _patientRepository.FirstOrDefaultAsync(p => p.Id == saveTask.PatientId);
        if (patient.IsAssignedToTik.HasValue && patient.IsAssignedToTik == true)
        {//Tik users task aç
         //Get tik users
            var tikUsers = await _userService.GetTikStaffListAsync();
            if (tikUsers?.Any() ?? false)
            {
                taskUsers.AddRange(tikUsers.Select(u => u.Id));
                toList.AddRange(tikUsers.Select(u => u.Email));
            }
        }
        else
        {//Operasyon kaydını oluşturan kullanıcıya task aç
            var operation = (await _operationRepository.WithDetailsAsync(o => o.Creator)).FirstOrDefault(o => o.Id == saveTask.RelatedEntityId);
            if (operation != null)
            {
                taskUsers.Add(operation.Creator.Id);
                toList.Add(operation.Creator.Email);
            }
        }

        //Create Task
        if (taskUsers?.Any() ?? false)
        {
            List<HTSTask> tasks = new List<HTSTask>();
            foreach (var user in taskUsers)
            {
                tasks.Add(new HTSTask()
                {
                    TaskTypeId = EntityEnum.TaskTypeEnum.PatientApproval.GetHashCode(),
                    UserId = user,
                    IsActive = true,
                    PatientId = saveTask.PatientId,
                    RelatedEntityId = saveTask.RelatedEntityId,
                    Url = "https://webhts.ushas.com.tr/patient/edit/" + saveTask.PatientId
                });
            }
            await _taskRepository.InsertManyAsync(tasks);
            //Send Email
            await SendEMailToPatientApprovalTaskUsers(toList, saveTask);
        }
    }

    public async Task CloseTask(SaveHTSTaskDto saveTask)
    {
        //Close tasks
        var tasks = (await _taskRepository.GetQueryableAsync()).Where(t =>
            t.TaskTypeId == saveTask.TaskType.GetHashCode()
            && t.RelatedEntityId == saveTask.RelatedEntityId
            && t.IsActive == true)
            .ToList();
        if (tasks?.Any() ?? false)
        {
            tasks = tasks.Select(t =>
            {
                t.IsActive = false;
                return t;
            }).ToList();
            await _taskRepository.UpdateManyAsync(tasks);
        }
    }


    /// <summary>
    /// Checks if data is valid to assign to tik
    /// </summary>
    /// <param name="patient">To be checked object</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private void IsDataValidToAssignToTik([CanBeNull] Patient patient)
    {
        if (patient == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }
        if (patient.IsAssignedToTik == true)
        {
            throw new HTSBusinessException(ErrorCode.AlreadyAssignedToTik);
        }
    }

    /// <summary>
    ///  Checks if data is valid to return from tik
    /// </summary>
    /// <param name="patient">To be checked object</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private void IsDataValidToReturnFromTik(Patient patient)
    {
        if (patient == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }
        if (patient.IsAssignedToTik == false)
        {
            throw new HTSBusinessException(ErrorCode.PatinentNotSetAsAssignedToTik);
        }
    }


    private void SendEMailToTikUsers(List<string> toUsers, Patient patient)
    {
        //Send mail to hospital consultations
        string mailBodyFormat = string.Format(_localizer["SendToTik:MailBody"],
                                    $"{patient.Name} {patient.Surname}");
        Helper.SendMail(toUsers, mailBodyFormat, null, _localizer["SendToTik:MailSubject"]);
    }

    private async Task SendEMailToPricerUsers(List<string> toUsers, SaveHTSTaskDto saveTask)
    {
        //Send mail to hospital consultations
        var patient = await _patientRepository.FirstOrDefaultAsync(p => p.Id == saveTask.PatientId);
        string mailBodyFormat = string.Format(_localizer["PricerTask:MailBody"],
            $"{patient.Name} {patient.Surname}", saveTask.TreatmentCode);
        Helper.SendMail(toUsers, mailBodyFormat, null, _localizer["PricerTask:MailSubject"]);
    }
    
    private async Task SendEMailToPatientApprovalTaskUsers(List<string> toUsers, SaveHTSTaskDto saveTask)
    {
        //Send mail
        var patient = await _patientRepository.FirstOrDefaultAsync(p => p.Id == saveTask.PatientId);
        string mailBodyFormat = string.Format(_localizer["PatientApprovalTask:MailBody"],
            $"{patient.Name} {patient.Surname}", saveTask.TreatmentCode);
        Helper.SendMail(toUsers, mailBodyFormat, null, _localizer["PatientApprovalTask:MailSubject"]);
    }

}