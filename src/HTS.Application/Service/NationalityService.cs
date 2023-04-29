using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Nationality;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class NationalityService : ApplicationService, INationalityService
{
    private readonly IRepository<Nationality, int> _nationalityRepository;
    public NationalityService(IRepository<Nationality, int> nationalityRepository) 
    {
        _nationalityRepository = nationalityRepository;
    }
    
    public async Task<NationalityDto> GetAsync(int id)
    {
        return ObjectMapper.Map<Nationality, NationalityDto>(await _nationalityRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<NationalityDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _nationalityRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            n => n.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<Nationality>, List<NationalityDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _nationalityRepository.CountAsync();//item count
        return new PagedResultDto<NationalityDto>(totalCount,responseList);
    }

    public async Task<NationalityDto> CreateAsync(SaveNationalityDto nationality)
    {
        var entity = ObjectMapper.Map<SaveNationalityDto, Nationality>(nationality);
        await _nationalityRepository.InsertAsync(entity);
        return ObjectMapper.Map<Nationality, NationalityDto>(entity);
    }

    public async Task<NationalityDto> UpdateAsync(int id, SaveNationalityDto nationality)
    {
        var entity = await _nationalityRepository.GetAsync(id);
        ObjectMapper.Map(nationality, entity);
        return ObjectMapper.Map<Nationality,NationalityDto>( await _nationalityRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _nationalityRepository.DeleteAsync(id);
    }
}