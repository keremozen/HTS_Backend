using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Process;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class ProcessService : ApplicationService, IProcessService
{
    private readonly IRepository<Process, int> _processRepository;
    public ProcessService(IRepository<Process, int> processRepository) 
    {
        _processRepository = processRepository;
    }
    
    public async Task<ProcessDto> GetAsync(int id)
    {
        return ObjectMapper.Map<Process, ProcessDto>(await _processRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<ProcessDto>> GetListAsync()
    {
        //Get all entities
        var query = await _processRepository.WithDetailsAsync();
        var responseList = ObjectMapper.Map<List<Process>, List<ProcessDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _processRepository.CountAsync();//item count
        return new PagedResultDto<ProcessDto>(totalCount,responseList);
    }

    public async Task<ProcessDto> CreateAsync(SaveProcessDto process)
    {
        var entity = ObjectMapper.Map<SaveProcessDto, Process>(process);
        await _processRepository.InsertAsync(entity);
        return ObjectMapper.Map<Process, ProcessDto>(entity);
    }

    public async Task<ProcessDto> UpdateAsync(int id, SaveProcessDto process)
    {
        var entity = await _processRepository.GetAsync(id);
        ObjectMapper.Map(process, entity);
        return ObjectMapper.Map<Process,ProcessDto>( await _processRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _processRepository.DeleteAsync(id);
    }
}