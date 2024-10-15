using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientNote;
using HTS.Dto.SalesMethodAndCompanionInfo;
using HTS.Enum;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;
[Authorize]
public class SalesMethodAndCompanionInfoService : ApplicationService, ISalesMethodAndCompanionInfoService
{
    private readonly IRepository<PatientTreatmentProcess, int> _ptpRepository;
    private readonly IRepository<SalesMethodAndCompanionInfo, int> _salesMethodAndCompanionInfoRepository;
    private readonly IIdentityUserRepository _userRepository;
    private readonly IRepository<InterpreterAppointment, int> _interpreterAppointmentRepository;
    private readonly IUserService _userService;
    private IConfiguration _config;
    private readonly IRepository<HTSTask, int> _taskRepository;
    public SalesMethodAndCompanionInfoService(IRepository<SalesMethodAndCompanionInfo, int> salesMethodAndCompanionInfoRepository,
        IIdentityUserRepository userRepository,
        IRepository<InterpreterAppointment, int> interpreterAppointmentRepository,
        IRepository<PatientTreatmentProcess, int> ptpRepository,
        IRepository<HTSTask, int> taskRepository,
        IUserService userService,
        IConfiguration config)
    {
        _salesMethodAndCompanionInfoRepository = salesMethodAndCompanionInfoRepository;
        _userRepository = userRepository;
        _interpreterAppointmentRepository = interpreterAppointmentRepository;
        _ptpRepository = ptpRepository;
        _taskRepository = taskRepository;
        _userService = userService;
        _config = config;
    }

    public async Task<SalesMethodAndCompanionInfoDto> GetByPatientTreatmentProcessIdAsync(int ptpId)
    {
        try
        {
            var response =await (await _salesMethodAndCompanionInfoRepository.GetQueryableAsync())
                .AsNoTracking()
                .Include(s => s.InterpreterAppointments)
                .ThenInclude(a=>a.Branch)
                .FirstOrDefaultAsync(i => i.PatientTreatmentProcessId == ptpId);
            return ObjectMapper.Map<SalesMethodAndCompanionInfo, SalesMethodAndCompanionInfoDto>(response);
        }
        catch (EntityNotFoundException)
        {
            return null;
        }
    }

    public async Task<SalesMethodAndCompanionInfoDto> SaveAsync(SaveSalesMethodAndCompanionInfoDto salesMethodAndCompanionInfo)
    {
        var entity = await _salesMethodAndCompanionInfoRepository.FirstOrDefaultAsync(i =>
                  i.PatientTreatmentProcessId == salesMethodAndCompanionInfo.PatientTreatmentProcessId);
        if (entity != null)
        {//Update process
            //Delete child records
            if (entity.InterpreterAppointments != null)
            {
                await _interpreterAppointmentRepository.DeleteManyAsync(entity.InterpreterAppointments.Select(a => a.Id)
                    .ToList());
            }

            ObjectMapper.Map(salesMethodAndCompanionInfo, entity);
            await _salesMethodAndCompanionInfoRepository.UpdateAsync(entity);
        }
        else
        {
            entity = ObjectMapper.Map<SaveSalesMethodAndCompanionInfoDto, SalesMethodAndCompanionInfo>(salesMethodAndCompanionInfo);
            await _salesMethodAndCompanionInfoRepository.InsertAsync(entity);
        }
        
        if (entity.IsDocumentTranslationRequired)//Create task
        {
            await CreateTaskDocumentTranslationRequired(salesMethodAndCompanionInfo.PatientTreatmentProcessId);
        }
       
        return ObjectMapper.Map<SalesMethodAndCompanionInfo, SalesMethodAndCompanionInfoDto>(entity);
    }

    private async Task CreateTaskDocumentTranslationRequired(int ptpId)
    {
        var patientTreatmentProcess = await (await _ptpRepository.GetQueryableAsync())
            .AsNoTracking()
            .Include(p => p.Creator)
            .Include(p => p.Patient)
            .FirstOrDefaultAsync(t => t.Id == ptpId);
        //Tedavi kaydını oluşturan kullanıcıya task açılır. (Hasta TİK'e devredildiyse TİK rolündeki kullanıcılar)
        string urlFormat = _config["ServiceURL:TaskUrlFormat"];
        string taskUrl = string.Format(urlFormat, patientTreatmentProcess.PatientId);
        
        if (patientTreatmentProcess.Patient.IsAssignedToTik ?? false)
        {
            //Get tik users
            var tikUsers = await _userService.GetTikStaffListAsync();
            if (!(tikUsers?.Any() ?? false))
            {
                return;
            }

            //Create task
            List<HTSTask> tasks = new List<HTSTask>();
            foreach (var tik in tikUsers)
            {
                tasks.Add(GetTask(tik.Id, patientTreatmentProcess, taskUrl));
            }
            await _taskRepository.InsertManyAsync(tasks);
        }
        else
        {
            var task = GetTask(patientTreatmentProcess.Creator.Id, patientTreatmentProcess, taskUrl);
            await _taskRepository.InsertAsync(task);
        }
    }

    private static HTSTask GetTask(Guid userId, PatientTreatmentProcess patientTreatmentProcess,
        string taskUrl)
    {
        return new HTSTask()
        {
            TaskTypeId = TaskTypeEnum.DocumentTranslate.GetHashCode(),
            UserId = userId,
            IsActive = true,
            PatientId = patientTreatmentProcess.PatientId,
            RelatedEntityId = patientTreatmentProcess.Id,
            Url = taskUrl
        };
    }
}