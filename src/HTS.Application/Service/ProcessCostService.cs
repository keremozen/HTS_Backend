using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ProcessCost;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
[Authorize("HTS.ProcessCostManagement")]
public class ProcessCostService : ApplicationService, IProcessCostService
{
    private readonly IRepository<ProcessCost, int> _processCostRepository;
    public ProcessCostService(IRepository<ProcessCost, int> processCostRepository) 
    {
        _processCostRepository = processCostRepository;
    }
    
    public async Task<ProcessCostDto> GetAsync(int id)
    {
        return ObjectMapper.Map<ProcessCost, ProcessCostDto>(await _processCostRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<ProcessCostDto>> GetListAsync(bool? isActive=null)
    {
        //Get all entities
        var query = await _processCostRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<ProcessCost>, List<ProcessCostDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _processCostRepository.CountAsync();//item count
        return new PagedResultDto<ProcessCostDto>(totalCount,responseList);
    }

    public async Task<ProcessCostDto> CreateAsync(SaveProcessCostDto processCost)
    {
        var entity = ObjectMapper.Map<SaveProcessCostDto, ProcessCost>(processCost);
        await _processCostRepository.InsertAsync(entity);
        return ObjectMapper.Map<ProcessCost, ProcessCostDto>(entity);
    }

    public async Task<ProcessCostDto> UpdateAsync(int id, SaveProcessCostDto processCost)
    {
        var entity = await _processCostRepository.GetAsync(id);
        ObjectMapper.Map(processCost, entity);
        return ObjectMapper.Map<ProcessCost,ProcessCostDto>( await _processCostRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _processCostRepository.DeleteAsync(id);
    }
}