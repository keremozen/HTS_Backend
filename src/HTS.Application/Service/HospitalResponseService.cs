using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalResponse;
using HTS.Enum;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class HospitalResponseService : ApplicationService, IHospitalResponseService
{
    private readonly IRepository<HospitalResponse, int> _hospitalResponseRepository;
    private readonly IRepository<HospitalConsultation, int> _hcRepository;
    public HospitalResponseService(IRepository<HospitalResponse, int> hospitalResponseRepository,
        IRepository<HospitalConsultation, int> hcRepository) 
    {
        _hospitalResponseRepository = hospitalResponseRepository;
        _hcRepository = hcRepository;
    }
    
    public async Task<HospitalResponseDto> GetAsync(int id)
    {
        var query = (await _hospitalResponseRepository.WithDetailsAsync()).Where(r => r.Id == id);
        return ObjectMapper.Map<HospitalResponse, HospitalResponseDto>(await AsyncExecuter.FirstOrDefaultAsync(query));
    }
    

    public async Task CreateAsync(SaveHospitalResponseDto hospitalResponse)
    {
       await IsDataValidToSave(hospitalResponse);
        var entity = ObjectMapper.Map<SaveHospitalResponseDto, HospitalResponse>(hospitalResponse);
        if (hospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            
        }
        else
        {
            hospitalResponse.PossibleTreatmentDate = null;
            hospitalResponse.HospitalResponseBranches = null;
            hospitalResponse.HospitalResponseProcesses = null;
            hospitalResponse.HospitalResponseMaterials = null;
        }
        await _hospitalResponseRepository.InsertAsync(entity);
    }

    public async Task ApproveAsync(int hospitalResponseId)
    {
        var hr = (await _hospitalResponseRepository.WithDetailsAsync(hr => hr.HospitalConsultation))
            .Where(hr => hr.Id == hospitalResponseId);
       var entity = await AsyncExecuter.FirstOrDefaultAsync(hr);
       await IsDataValidToApprove(entity);
       entity.HospitalConsultation.HospitalConsultationStatusId =
           EntityEnum.HospitalConsultationStatusEnum.OperationApproved.GetHashCode();
      await _hospitalResponseRepository.UpdateAsync(entity);
    }
    
    public async Task RejectAsync(int hospitalResponseId)
    {
        var hr = (await _hospitalResponseRepository.WithDetailsAsync(hr => hr.HospitalConsultation))
            .Where(hr => hr.Id == hospitalResponseId);
        var entity = await AsyncExecuter.FirstOrDefaultAsync(hr);
        await IsDataValidToReject(entity);
        entity.HospitalConsultation.HospitalConsultationStatusId =
            EntityEnum.HospitalConsultationStatusEnum.OperationRejected.GetHashCode();
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
        if (await _hcRepository.AnyAsync(c => c.Id == hospitalResponse.HospitalConsultationId))
        {
            throw new HTSBusinessException(ErrorCode.RelationalDataIsMissing);
        }
        
        //There should be only one hospital response
        if (await  _hospitalResponseRepository.AnyAsync(r => r.HospitalConsultationId == hospitalResponse.HospitalConsultationId))
        {
            throw new HTSBusinessException(ErrorCode.HospitalAlreadyResponsed);
        }
        //TODO:Hopsy response should be correct hospital, authorized uhb user. In the future chech this.
     
        //If status is ok, check data
        if (hospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            if (!hospitalResponse.HospitalizationTypeId.HasValue
                 || !(hospitalResponse.HospitalResponseBranches?.Any() ?? false)
                || ( hospitalResponse.HospitalizationTypeId == EntityEnum.HospitalizationTypeEnum.SurgicalHospitalization.GetHashCode() && !(hospitalResponse.HospitalResponseProcesses?.Any() ?? false))
                ||  !(hospitalResponse.HospitalResponseMaterials?.Any() ?? false)
                || hospitalResponse.PossibleTreatmentDate == DateTime.MinValue
                || hospitalResponse.PossibleTreatmentDate == null
                || hospitalResponse.HospitalizationNumber == null)
            {
                throw new HTSBusinessException(ErrorCode.RequiredFieldsMissingForSuitableForTreatment);
            }
        }
    }

}