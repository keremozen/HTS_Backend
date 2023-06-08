using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.Operation;
using HTS.Enum;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using static HTS.Enum.EntityEnum;
namespace HTS.Service;

public class OperationService : ApplicationService, IOperationService
{
    private readonly IRepository<Operation, int> _operationRepository;

    public OperationService(IRepository<Operation, int> operationRepository)
    {
        _operationRepository = operationRepository;
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
        entity.OperationTypeId = OperationTypeEnum.Manual.GetHashCode();
        await _operationRepository.InsertAsync(entity);
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
                || operation.HospitalResponse.PossibleTreatmentDate == null
                || operation.HospitalResponse.HospitalizationNumber == null)
            {
                throw new HTSBusinessException(ErrorCode.RequiredFieldsMissingForSuitableForTreatment);
            }
        }
    }


}