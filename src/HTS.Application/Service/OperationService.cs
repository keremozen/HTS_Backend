using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.Operation;
using HTS.Enum;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using static HTS.Enum.EntityEnum;
namespace HTS.Service;
[Authorize]
public class OperationService : ApplicationService, IOperationService
{
    private readonly IRepository<Operation, int> _operationRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;

    public OperationService(IRepository<Operation, int> operationRepository,
        IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository)
    {
        _operationRepository = operationRepository;
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
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
        ptp.TreatmentProcessStatusId =PatientTreatmentStatusEnum.OperationApprovedWaitingPricing.GetHashCode();
        await _patientTreatmentProcessRepository.UpdateAsync(ptp);
    }

    public async Task UpdateAsync(int id, SaveOperationDto operation)
    {
        operation.HospitalResponse = null;
        var entity = await _operationRepository.GetAsync(id);
        ObjectMapper.Map(operation, entity);
        await _operationRepository.UpdateAsync(entity);
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