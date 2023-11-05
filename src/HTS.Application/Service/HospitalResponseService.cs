using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Common;
using HTS.Data.Entity;
using HTS.Dto.Branch;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalResponse;
using HTS.Dto.HospitalResponseBranch;
using HTS.Dto.HospitalResponseProcess;
using HTS.Dto.HTSTask;
using HTS.Enum;
using HTS.Interface;
using HTS.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class HospitalResponseService : ApplicationService, IHospitalResponseService
{
    private readonly IRepository<HospitalResponse, int> _hospitalResponseRepository;
    private readonly IRepository<HospitalResponseType, int> _responseTypeRepository;
    private readonly IRepository<HospitalConsultation, int> _hcRepository;
    private readonly IRepository<Operation, int> _operationRepository;
    private readonly IStringLocalizer<HTSResource> _localizer;
    private readonly IHTSTaskService _htsTaskService;

    public HospitalResponseService(IRepository<HospitalResponse, int> hospitalResponseRepository,
        IRepository<HospitalResponseType, int> hospitalResponseTypeRepository,
        IRepository<HospitalConsultation, int> hcRepository,
        IRepository<Operation, int> operationRepository,
        IStringLocalizer<HTSResource> localizer,
        IHTSTaskService htsTaskService
        )
    {
        _hospitalResponseRepository = hospitalResponseRepository;
        _responseTypeRepository = hospitalResponseTypeRepository;
        _hcRepository = hcRepository;
        _operationRepository = operationRepository;
        _localizer = localizer;
        _htsTaskService = htsTaskService;
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

        var hConsultation = hospitalResponse.HospitalConsultationId.HasValue ?
            (await _hcRepository.WithDetailsAsync((hc => hc.Hospital), (hc => hc.PatientTreatmentProcess.Patient), (hc => hc.HospitalResponses), (hc => hc.Creator))).FirstOrDefault(hc => hc.Id == hospitalResponse.HospitalConsultationId.Value) :
            null;
        if (hospitalResponse.HospitalResponseTypeId == HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            hConsultation.HospitalConsultationStatusId = HospitalConsultationStatusEnum.SuitableForTreatment.GetHashCode();
        }
        else if (hospitalResponse.HospitalResponseTypeId == HospitalResponseTypeEnum.NotSuitableForTreatment.GetHashCode())
        {
            hConsultation.HospitalConsultationStatusId = HospitalConsultationStatusEnum.NotSuitableForTreatment.GetHashCode();
        }
        else if (hospitalResponse.HospitalResponseTypeId == HospitalResponseTypeEnum.ExaminationsIsRequiredForDiagnosis.GetHashCode())
        {
            hConsultation.HospitalConsultationStatusId = HospitalConsultationStatusEnum.ExaminationsIsRequiredForDiagnosis.GetHashCode();
        }
        //Update patient treatment process entity status clm - "Hastanelere Danışıldı - Değerlendirme Bekliyor"
        hConsultation.PatientTreatmentProcess.TreatmentProcessStatusId =
           PatientTreatmentStatusEnum.HospitalAskedWaitingAssessment.GetHashCode();

        hConsultation.HospitalResponses.Add(entity);
        await _hcRepository.UpdateAsync(hConsultation);

        //hastane danışma kaydını oluşturan kullanıcıya mail atılacak
        await SendEMailToHospitalStaff(hospitalResponse.HospitalResponseTypeId, hConsultation);
    }

    private async Task SendEMailToHospitalStaff(int responseTypeId, HospitalConsultation consultation)
    {
        var responseType = (await _responseTypeRepository.WithDetailsAsync()).FirstOrDefault(hr => hr.Id == responseTypeId);
        //Send mail to hospital consultations
        string mailBodyFormat = string.Format(_localizer["HospitalResponseCompleted:MailBody"],
                                    consultation.PatientTreatmentProcess.Patient.Name + " " + consultation.PatientTreatmentProcess.Patient.Surname,
                                    consultation.Hospital.Name,
                                    responseType?.Name);
        Helper.SendMail(consultation.Creator.Email, mailBodyFormat, null, _localizer["HospitalResponseCompleted:MailSubject"]);
    }

    [Authorize]
    public async Task ApproveAsync(int hospitalResponseId)
    {
        var hr = (await _hospitalResponseRepository.WithDetailsAsync((hr => hr.HospitalConsultation),
                (hr => hr.HospitalConsultation.PatientTreatmentProcess)))
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
       operation= await _operationRepository.InsertAsync(operation, true);
        //Create Pricing Task
        await _htsTaskService.CreateAsync(new SaveHTSTaskDto()
        {
            HospitalId = entity.HospitalConsultation.HospitalId,
            RelatedEntityId = operation.Id,
            TaskType = TaskTypeEnum.Pricing,
            TreatmentCode = entity.HospitalConsultation.PatientTreatmentProcess.TreatmentCode,
            PatientId = entity.HospitalConsultation.PatientTreatmentProcess.PatientId
        });
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