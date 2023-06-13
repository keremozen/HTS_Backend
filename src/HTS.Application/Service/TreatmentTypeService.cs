using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.TreatmentType;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
[Authorize]
public class TreatmentTypeService : ApplicationService, ITreatmentTypeService
{
    private readonly IRepository<TreatmentType, int> _treatmentTypeRepository;
    public TreatmentTypeService(IRepository<TreatmentType, int> treatmentTypeRepository) 
    {
        _treatmentTypeRepository = treatmentTypeRepository;
    }
    
    public async Task<TreatmentTypeDto> GetAsync(int id)
    {
        return ObjectMapper.Map<TreatmentType, TreatmentTypeDto>(await _treatmentTypeRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<TreatmentTypeDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _treatmentTypeRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<TreatmentType>, List<TreatmentTypeDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _treatmentTypeRepository.CountAsync();//item count
        return new PagedResultDto<TreatmentTypeDto>(totalCount,responseList);
    }

    public async Task<TreatmentTypeDto> CreateAsync(SaveTreatmentTypeDto treatmentType)
    {
        var entity = ObjectMapper.Map<SaveTreatmentTypeDto, TreatmentType>(treatmentType);
        await _treatmentTypeRepository.InsertAsync(entity);
        return ObjectMapper.Map<TreatmentType, TreatmentTypeDto>(entity);
    }

    public async Task<TreatmentTypeDto> UpdateAsync(int id, SaveTreatmentTypeDto treatmentType)
    {
        var entity = await _treatmentTypeRepository.GetAsync(id);
        ObjectMapper.Map(treatmentType, entity);
        return ObjectMapper.Map<TreatmentType,TreatmentTypeDto>( await _treatmentTypeRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _treatmentTypeRepository.DeleteAsync(id);
    }
}