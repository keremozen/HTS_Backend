using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using HTS.BusinessException;
using HTS.Common;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HTSTask;
using HTS.Dto.Operation;
using HTS.Enum;
using HTS.Interface;
using HTS.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

[Authorize]
public class OperationService : ApplicationService, IOperationService
{
    private readonly IRepository<Operation, int> _operationRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;
    private readonly IRepository<HospitalResponseBranch, int> _hospitalResponseBranchRepository;
    private readonly IRepository<HospitalResponseProcess, int> _hospitalResponseProcessRepository;
    private readonly IHTSTaskService _htsTaskService;
    private readonly IStringLocalizer<HTSResource> _localizer;

    public OperationService(IRepository<Operation, int> operationRepository,
        IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository,
        IRepository<HospitalResponseBranch, int> hospitalResponseBranchRepository,
        IRepository<HospitalResponseProcess, int> hospitalResponseProcessRepository,
        IHTSTaskService htsTaskService,
        IStringLocalizer<HTSResource> localizer)
    {
        _operationRepository = operationRepository;
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
        _hospitalResponseBranchRepository = hospitalResponseBranchRepository;
        _hospitalResponseProcessRepository = hospitalResponseProcessRepository;
        _htsTaskService = htsTaskService;
        _localizer = localizer;
    }

    public async Task<OperationDto> GetAsync(int id)
    {
        var query = (await _operationRepository.WithDetailsAsync()).Where(o => o.Id == id);
        var operations = await AsyncExecuter.FirstOrDefaultAsync(query);
        return ObjectMapper.Map<Operation, OperationDto>(operations);
    }
    public async Task<PagedResultDto<OperationDto>> GetByPatientTreatmenProcessAsync(int ptpId)
    {
        var query = await _operationRepository.WithDetailsAsync();
        query = query.Where(o => o.PatientTreatmentProcessId == ptpId);

        var responseList = ObjectMapper.Map<List<Operation>, List<OperationDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _operationRepository.CountAsync();//item count

        return new PagedResultDto<OperationDto>(totalCount, responseList);
    }


    public async Task CreateAsync(SaveOperationDto operation)
    {
        IsDataValidToSave(operation);
        var entity = ObjectMapper.Map<SaveOperationDto, Operation>(operation);
        entity.OperationStatusId = OperationStatusEnum.NewRecord.GetHashCode();
        entity.OperationTypeId = OperationTypeEnum.Manual.GetHashCode();
        await _operationRepository.InsertAsync(entity);
        //Tedavi sürecini, operasyon onaylandı fiyatlandırma bekliyor olarak güncelle
        var ptp = await _patientTreatmentProcessRepository.GetAsync(operation.PatientTreatmentProcessId.Value);
        ptp.TreatmentProcessStatusId = PatientTreatmentStatusEnum.OperationApprovedWaitingPricing.GetHashCode();
        await _patientTreatmentProcessRepository.UpdateAsync(ptp);
    }

    public async Task UpdateAsync(int id, SaveOperationDto operation)
    {
        var entity = (await _operationRepository.WithDetailsAsync((o=>o.HospitalResponse.HospitalResponseBranches), (o=>o.HospitalResponse.HospitalResponseProcesses))).FirstOrDefault(o => o.Id == id);
        if (entity.OperationStatusId != OperationStatusEnum.NewRecord.GetHashCode())
        {
            operation.HospitalResponse = null;
        }
        else
        {
            await _hospitalResponseBranchRepository.DeleteManyAsync(entity.HospitalResponse.HospitalResponseBranches.Select(b => b.Id).ToList());
            await _hospitalResponseProcessRepository.DeleteManyAsync(entity.HospitalResponse.HospitalResponseProcesses.Select(p => p.Id).ToList());
        }
        ObjectMapper.Map(operation, entity);
        await _operationRepository.UpdateAsync(entity);
    }

    public async Task SendToPricing(int id)
    {
        var entity = await _operationRepository.GetAsync(id);
        IsDataValidToSendToPricing(entity);
        entity.OperationStatusId = OperationStatusEnum.PriceExpecting.GetHashCode();
        await _operationRepository.UpdateAsync(entity);
       //Create Task
       var operationWithRelation = (await _operationRepository.WithDetailsAsync(
           (o => o.PatientTreatmentProcess),
           (o => o.PatientTreatmentProcess.Patient),
           o => o.Hospital,
           o => o.Hospital.HospitalStaffs,
           o => o.Hospital.HospitalStaffs.Select(hs => hs.User)))
           .AsNoTracking()
           .FirstOrDefault(o => o.Id == id);
      
       await _htsTaskService.CreateAsync(new SaveHTSTaskDto()
       {
           HospitalId = entity.HospitalId,
           RelatedEntityId = id,
           TaskType = TaskTypeEnum.Pricing,
           TreatmentCode = operationWithRelation?.PatientTreatmentProcess?.TreatmentCode,
           PatientId = operationWithRelation.PatientTreatmentProcess.PatientId
       });
       //Send email to hospital staff
       SendEMailToHospitalStaff(operationWithRelation);
    }

    /// <summary>
    /// If operation has hospital, send email to hospital staff
    /// </summary>
    /// <param name="operation"></param>
    private void SendEMailToHospitalStaff(Operation operation)
    {
        if (operation.HospitalId is null)
        {//no emails to send
            return;
        }

        var toList = operation.Hospital?.HospitalStaffs.Select(s => s.User.Email).ToList();
        if (toList?.Any() ?? false)
        {
            var patientName = $"{operation.PatientTreatmentProcess?.Patient.Name} {operation.PatientTreatmentProcess?.Patient.Surname}";    
            string mailBody = string.Format(_localizer["SendToPricing:MailBody"],patientName);
            var mailSubject =string.Format(_localizer["SendToPricing:MailSubject"],patientName);
            Helper.SendMail(toList, mailBody,file:null, subject: mailSubject, fileName:null);
        }
      
    }

    /// <summary>
    /// Checks if data is valid to send to pricing
    /// </summary>
    /// <param name="entity">To be checked entity</param>
    /// <exception cref="HTSBusinessException"></exception>
    private void IsDataValidToSendToPricing(Operation entity)
    {
        if (entity == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }

        if (entity.OperationStatusId != OperationStatusEnum.NewRecord.GetHashCode())
        {
            throw new HTSBusinessException(ErrorCode.OperationStatusNotValid);
        }
    }

    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="operation">To be saved object</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private void IsDataValidToSave(SaveOperationDto operation)
    {
        //If status is ok, check data
        if (operation.HospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            if (!operation.HospitalResponse.HospitalizationTypeId.HasValue
                || !(operation.HospitalResponse.HospitalResponseBranches?.Any() ?? false)
                || (operation.HospitalResponse.HospitalizationTypeId == EntityEnum.HospitalizationTypeEnum.SurgicalHospitalization.GetHashCode() && !(operation.HospitalResponse.HospitalResponseProcesses?.Any() ?? false))
                || operation.HospitalResponse.PossibleTreatmentDate == DateTime.MinValue
                || operation.HospitalResponse.HospitalizationNumber == null)
            {
                throw new HTSBusinessException(ErrorCode.RequiredFieldsMissingForSuitableForTreatment);
            }
        }
    }


}