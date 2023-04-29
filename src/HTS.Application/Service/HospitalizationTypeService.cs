using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.HospitalizationType;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class HospitalizationTypeService : ApplicationService, IHospitalizationTypeService
{
    private readonly IRepository<HospitalizationType, int> _hospitalizationTypeRepository;
    public HospitalizationTypeService(IRepository<HospitalizationType, int> hospitalizationTypeRepository) 
    {
        _hospitalizationTypeRepository = hospitalizationTypeRepository;
    }

    public async Task<HospitalizationTypeDto> GetAsync(int id)
    {
        return ObjectMapper.Map<HospitalizationType, HospitalizationTypeDto>(await _hospitalizationTypeRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<HospitalizationTypeDto>> GetListAsync(bool? isActive=null)
    {
        //Get all entities
        var query = await _hospitalizationTypeRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<HospitalizationType>, List<HospitalizationTypeDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _hospitalizationTypeRepository.CountAsync();//item count
        return new PagedResultDto<HospitalizationTypeDto>(totalCount,responseList);
    }

    public async Task<HospitalizationTypeDto> CreateAsync(SaveHospitalizationTypeDto hospitalizationType)
    {
        var entity = ObjectMapper.Map<SaveHospitalizationTypeDto, HospitalizationType>(hospitalizationType);
        await _hospitalizationTypeRepository.InsertAsync(entity);
        return ObjectMapper.Map<HospitalizationType, HospitalizationTypeDto>(entity);
    }

    public async Task<HospitalizationTypeDto> UpdateAsync(int id, SaveHospitalizationTypeDto hospitalizationType)
    {
        var entity = await _hospitalizationTypeRepository.GetAsync(id);
        ObjectMapper.Map(hospitalizationType, entity);
        return ObjectMapper.Map<HospitalizationType, HospitalizationTypeDto>( await _hospitalizationTypeRepository.UpdateAsync(entity));
    }

    public async Task DeleteAsync(int id)
    {
        await _hospitalizationTypeRepository.DeleteAsync(id);
    }
}