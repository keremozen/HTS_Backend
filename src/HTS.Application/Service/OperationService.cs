using System;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.Operation;
using HTS.Enum;
using HTS.Interface;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static HTS.Enum.EntityEnum;
namespace HTS.Service;

public class OperationService : ApplicationService, IOperationService
{
    private readonly IRepository<Operation, int> _operationRepository;

    public OperationService(IRepository<Operation, int> operationRepository)
    {
        _operationRepository = operationRepository;
    }
    

    public async Task CreateAsync(SaveOperationDto operation)
    {
        await IsDataValidToSave(operation);
        var entity = ObjectMapper.Map<SaveOperationDto, Operation>(operation);
        entity.OperationTypeId = OperationTypeEnum.Manual.GetHashCode();
        await _operationRepository.InsertAsync(entity);
    }
    
    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="operation">To be saved object</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async Task IsDataValidToSave(SaveOperationDto operation)
    {
        
        //If status is ok, check data
        if (operation.HospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
            if (!operation.HospitalResponse.HospitalizationTypeId.HasValue
                || !(operation.HospitalResponse.HospitalResponseBranches?.Any() ?? false)
                || (operation.HospitalResponse.HospitalizationTypeId == EntityEnum.HospitalizationTypeEnum.SurgicalHospitalization.GetHashCode() && !(operation.HospitalResponse.HospitalResponseProcesses?.Any() ?? false))
                || !(operation.HospitalResponse.HospitalResponseMaterials?.Any() ?? false)
                || operation.HospitalResponse.PossibleTreatmentDate == DateTime.MinValue
                || operation.HospitalResponse.PossibleTreatmentDate == null
                || operation.HospitalResponse.HospitalizationNumber == null)
            {
                throw new HTSBusinessException(ErrorCode.RequiredFieldsMissingForSuitableForTreatment);
            }
        }
    }


}