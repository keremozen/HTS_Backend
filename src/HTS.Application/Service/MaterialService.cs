using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Branch;
using HTS.Dto.Material;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class MaterialService : ApplicationService, IMaterialService
{
    private readonly IRepository<Material, int> _materialRepository;
    public MaterialService(IRepository<Material, int> materialRepository) 
    {
        _materialRepository = materialRepository;
    }
    
    public async Task<MaterialDto> GetAsync(int id)
    {
        return ObjectMapper.Map<Material, MaterialDto>(await _materialRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<MaterialDto>> GetListAsync(bool? isActive=null)
    {
        //Get all entities
        var query = await _materialRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        
        var responseList = ObjectMapper.Map<List<Material>, List<MaterialDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _materialRepository.CountAsync();//item count
        return new PagedResultDto<MaterialDto>(totalCount,responseList);
    }

    public async Task<MaterialDto> CreateAsync(SaveMaterialDto major)
    {
        var entity = ObjectMapper.Map<SaveMaterialDto, Material>(major);
        await _materialRepository.InsertAsync(entity);
        return ObjectMapper.Map<Material, MaterialDto>(entity);
    }

    public async Task<MaterialDto> UpdateAsync(int id, SaveMaterialDto material)
    {
        var entity = await _materialRepository.GetAsync(id);
        ObjectMapper.Map(material, entity);
        return ObjectMapper.Map<Material, MaterialDto>( await _materialRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _materialRepository.DeleteAsync(id);
    }
}