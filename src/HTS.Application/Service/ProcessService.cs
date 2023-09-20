using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Process;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class ProcessService : ApplicationService, IProcessService
{
    private readonly IRepository<Process, int> _processRepository;
    private readonly IRepository<ProcessCost, int> _processCostRepository;
    public ProcessService(IRepository<Process, int> processRepository, IRepository<ProcessCost, int> processCostRepository)
    {
        _processRepository = processRepository;
        _processCostRepository = processCostRepository;
    }

    public async Task<ProcessDto> GetAsync(int id)
    {
        return ObjectMapper.Map<Process, ProcessDto>(await _processRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<ProcessDto>> GetListAsync(bool? isActive = null)
    {
        //Get all entities
        var query = (await _processRepository.WithDetailsAsync()).WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<Process>, List<ProcessDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = responseList.Count;//item count
        return new PagedResultDto<ProcessDto>(totalCount, responseList);
    }

    public async Task<PagedResultDto<ProcessDto>> GetListByKeywordAsync(string keyword, int processTypeId, bool? isActive = null)
    {
        //Get all entities
        var query = (await _processRepository.WithDetailsAsync()).WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        query = query.Where(p => p.ProcessTypeId == processTypeId && (p.Code.ToUpper().Contains(keyword.ToUpper()) || 
                                                                      p.Name.ToUpper().Contains(keyword.ToUpper()) ||
                                                                      p.Code.ToLower().Contains(keyword.ToLower()) ||
                                                                      p.Name.ToLower().Contains(keyword.ToLower())));
        var responseList = ObjectMapper.Map<List<Process>, List<ProcessDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = responseList.Count;//item count
        return new PagedResultDto<ProcessDto>(totalCount, responseList);
    }

    [Authorize("HTS.ProcessManagement")]
    public async Task<ProcessDto> CreateAsync(SaveProcessDto process)
    {
        var entity = ObjectMapper.Map<SaveProcessDto, Process>(process);
        await _processRepository.InsertAsync(entity);
        return ObjectMapper.Map<Process, ProcessDto>(entity);
    }

    [Authorize("HTS.ProcessManagement")]
    public async Task<ProcessDto> UpdateAsync(int id, SaveProcessDto process)
    {
        var entity = await _processRepository.GetAsync(id);
        ObjectMapper.Map(process, entity);
        return ObjectMapper.Map<Process, ProcessDto>(await _processRepository.UpdateAsync(entity));
    }

    [Authorize("HTS.ProcessManagement")]
    public async Task DeleteAsync(int id)
    {
        await _processRepository.DeleteAsync(id);
    }
}