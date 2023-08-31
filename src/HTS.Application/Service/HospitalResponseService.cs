﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.Branch;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalResponse;
using HTS.Dto.HospitalResponseBranch;
using HTS.Dto.HospitalResponseProcess;
using HTS.Enum;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class HospitalResponseService : ApplicationService, IHospitalResponseService
{
    private readonly IRepository<HospitalResponse, int> _hospitalResponseRepository;
    private readonly IRepository<HospitalConsultation, int> _hcRepository;
    private readonly IRepository<Operation, int> _operationRepository;

    public HospitalResponseService(IRepository<HospitalResponse, int> hospitalResponseRepository,
        IRepository<HospitalConsultation, int> hcRepository,
        IRepository<Operation, int> operationRepository)
    {
        _hospitalResponseRepository = hospitalResponseRepository;
        _hcRepository = hcRepository;
        _operationRepository = operationRepository;
    }

    [Authorize]
    public async Task<HospitalResponseDto> GetAsync(int id)
    {
        var query = (await _hospitalResponseRepository.WithDetailsAsync()).Where(r => r.Id == id);
        return ObjectMapper.Map<HospitalResponse, HospitalResponseDto>(await AsyncExecuter.FirstOrDefaultAsync(query));
    }

    [Authorize]
    public async Task<HospitalResponseDto> GetByHospitalConsultationAsync(int consultationId)
    {
        var query = (await _hospitalResponseRepository.WithDetailsAsync()).Where(hr => hr.HospitalConsultationId == consultationId);
        return ObjectMapper.Map<HospitalResponse, HospitalResponseDto>(await AsyncExecuter.FirstOrDefaultAsync(query));
    }

    [AllowAnonymous]
    public async Task CreateAsync(SaveHospitalResponseDto hospitalResponse)
    {
        await IsDataValidToSave(hospitalResponse);
        var entity = ObjectMapper.Map<SaveHospitalResponseDto, HospitalResponse>(hospitalResponse);

        var hConsultation = hospitalResponse.HospitalConsultationId.HasValue ? await _hcRepository.GetAsync(hospitalResponse.HospitalConsultationId.Value) : null;
        if (hospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            hConsultation.HospitalConsultationStatusId = HospitalConsultationStatusEnum.SuitableForTreatment.GetHashCode();
        }
        else if (hospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.NotSuitableForTreatment.GetHashCode())
        {
            hConsultation.HospitalConsultationStatusId = HospitalConsultationStatusEnum.NotSuitableForTreatment.GetHashCode();
        }
        else if (hospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.ExaminationsIsRequiredForDiagnosis.GetHashCode())
        {
            hConsultation.HospitalConsultationStatusId = HospitalConsultationStatusEnum.ExaminationsIsRequiredForDiagnosis.GetHashCode();
        }
        //Update patient treatment process entity status clm - "Hastanelere Danışıldı - Değerlendirme Bekliyor"
        hConsultation.PatientTreatmentProcess.TreatmentProcessStatusId =
           PatientTreatmentStatusEnum.HospitalAskedWaitingAssessment.GetHashCode();

        hConsultation.HospitalResponses.Add(entity);
        await _hcRepository.UpdateAsync(hConsultation);
    }

    [Authorize]
    public async Task ApproveAsync(int hospitalResponseId)
    {
        var hr = (await _hospitalResponseRepository.WithDetailsAsync((hr => hr.HospitalConsultation), (hr => hr.HospitalConsultation.PatientTreatmentProcess)))
            .Where(hr => hr.Id == hospitalResponseId);
        var entity = await AsyncExecuter.FirstOrDefaultAsync(hr);
        await IsDataValidToApprove(entity);
        entity.HospitalConsultation.HospitalConsultationStatusId =
            EntityEnum.HospitalConsultationStatusEnum.OperationApproved.GetHashCode();
        //Update patient treatment process entity status clm - Operasyon Onaylandı Fiyatlandırma bekliyor
        entity.HospitalConsultation.PatientTreatmentProcess.TreatmentProcessStatusId =
            PatientTreatmentStatusEnum.OperationApprovedWaitingPricing.GetHashCode();
        await _hospitalResponseRepository.UpdateAsync(entity);

        //Insert operation record
        Operation operation = new Operation()
        {
            HospitalResponseId = hospitalResponseId,
            PatientTreatmentProcessId = entity.HospitalConsultation.PatientTreatmentProcess.Id,
            OperationTypeId = OperationTypeEnum.HospitalConsultation.GetHashCode(),
            OperationStatusId = OperationStatusEnum.PriceExpecting.GetHashCode()
        };
        await _operationRepository.InsertAsync(operation);
    }

    [Authorize]
    public async Task RejectAsync(int hospitalResponseId)
    {
        var hr = (await _hospitalResponseRepository.WithDetailsAsync(hr => hr.HospitalConsultation))
            .Where(hr => hr.Id == hospitalResponseId);
        var entity = await AsyncExecuter.FirstOrDefaultAsync(hr);
        await IsDataValidToReject(entity);
        entity.HospitalConsultation.HospitalConsultationStatusId = HospitalConsultationStatusEnum.OperationRejected.GetHashCode();
        await _hospitalResponseRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// Checks if entity valid to approve
    /// </summary>
    /// <param name="hospitalResponse">Hospital response entity</param>
    private async Task IsDataValidToApprove(HospitalResponse hospitalResponse)
    {
        if (hospitalResponse.HospitalResponseTypeId != EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            throw new HTSBusinessException(ErrorCode.HospitalResponseTypeNotValidToApprove);
        }
        //Check if any hospital consultation is approved in the same row number
        int rowNumber = hospitalResponse.HospitalConsultation.RowNumber;
        bool anyApprovedInGroup = await _hcRepository.AnyAsync(hc =>
            hc.PatientTreatmentProcessId == hospitalResponse.HospitalConsultation.PatientTreatmentProcessId
            && hc.RowNumber == rowNumber
            && hc.HospitalConsultationStatusId == HospitalConsultationStatusEnum.OperationApproved.GetHashCode());
        if (anyApprovedInGroup)//Approved before
        {
            throw new HTSBusinessException(ErrorCode.AnotherHospitalResponseIsApprovedInSameRowNumber);
        }
    }

    /// <summary>
    /// Checks if entity valid to reject
    /// </summary>
    /// <param name="hospitalResponse">Hospital response entity</param>
    private async Task IsDataValidToReject(HospitalResponse hospitalResponse)
    {
        if (hospitalResponse.HospitalResponseTypeId != EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            throw new HTSBusinessException(ErrorCode.HospitalResponseTypeNotValidToReject);
        }
    }


    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="hospitalResponse">To be saved object</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async Task IsDataValidToSave(SaveHospitalResponseDto hospitalResponse)
    {
        //Check hospital consultation is valid
        if (!await _hcRepository.AnyAsync(c => c.Id == hospitalResponse.HospitalConsultationId))
        {
            throw new HTSBusinessException(ErrorCode.RelationalDataIsMissing);
        }

        //There should be only one hospital response
        if (await _hospitalResponseRepository.AnyAsync(r => r.HospitalConsultationId == hospitalResponse.HospitalConsultationId))
        {
            throw new HTSBusinessException(ErrorCode.HospitalAlreadyResponsed);
        }
        //TODO:Hopsy response should be correct hospital, authorized uhb user. In the future chech this.

        //If status is ok, check data
        if (hospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            if (!hospitalResponse.HospitalizationTypeId.HasValue
                 || !(hospitalResponse.HospitalResponseBranches?.Any() ?? false)
                || (hospitalResponse.HospitalizationTypeId == EntityEnum.HospitalizationTypeEnum.SurgicalHospitalization.GetHashCode() && !(hospitalResponse.HospitalResponseProcesses?.Any() ?? false))
                || hospitalResponse.PossibleTreatmentDate == DateTime.MinValue
                || hospitalResponse.HospitalizationNumber == null)
            {
                throw new HTSBusinessException(ErrorCode.RequiredFieldsMissingForSuitableForTreatment);
            }
        }
    }

}