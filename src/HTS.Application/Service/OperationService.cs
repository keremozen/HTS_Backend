using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalResponse;
using HTS.Dto.HospitalResponseBranch;
using HTS.Dto.HospitalResponseMaterial;
using HTS.Dto.HospitalResponseProcess;
using HTS.Dto.Operation;
using HTS.Enum;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class OperationService : ApplicationService
{
    //TODO: Interface ekle
    private readonly IRepository<HospitalResponse, int> _hospitalResponseRepository;
    private readonly IRepository<HospitalConsultation, int> _hcRepository;
    private readonly IRepository<Operation, int> _operationRepository;

    public OperationService(IRepository<HospitalResponse, int> hospitalResponseRepository,
        IRepository<HospitalConsultation, int> hcRepository,
        IRepository<Operation, int> operationRepository)
    {
        _hospitalResponseRepository = hospitalResponseRepository;
        _hcRepository = hcRepository;
        _operationRepository = operationRepository;
    }
    

    public async Task CreateAsync(SaveOperationDto operation)
    {
        //TODO: IsDataValid ekle
        var entity = ObjectMapper.Map<SaveOperationDto, Operation>(operation);
        var hospitalResponse = entity.HospitalResponse;
        if (hospitalResponse.HospitalResponseTypeId == EntityEnum.HospitalResponseTypeEnum.SuitableForTreatment.GetHashCode())
        {
        }
        else
        {
            entity.HospitalResponse.PossibleTreatmentDate = null;
            entity.HospitalResponse.HospitalResponseBranches = null;
            entity.HospitalResponse.HospitalResponseProcesses = null;
            entity.HospitalResponse.HospitalResponseMaterials = null;
        }
        await _operationRepository.InsertAsync(entity);
    }

}