using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.TreatmentProcessStatus;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace HTS.Service;

public class TreatmentProcessStatusService : ApplicationService, ITreatmentProcessStatusService
{
    private readonly IRepository<TreatmentProcessStatus, int> _treatmentProcessStatusRepository;
    public TreatmentProcessStatusService(IRepository<TreatmentProcessStatus, int> treatmentProcessStatusRepository) 
    {
        _treatmentProcessStatusRepository = treatmentProcessStatusRepository;
    }
    
    public async Task<ListResultDto<TreatmentProcessStatusDto>> GetListAsync()
    {
        var responseList = ObjectMapper.Map<List<TreatmentProcessStatus>, List<TreatmentProcessStatusDto>>(await _treatmentProcessStatusRepository.GetListAsync());
        //Return the result
        return new ListResultDto<TreatmentProcessStatusDto>(responseList);
    }
    
}