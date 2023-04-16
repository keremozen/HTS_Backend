using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ProcessType;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class ProcessTypeService : ApplicationService, IProcessTypeService
{
    private readonly IRepository<ProcessType, int> _processTypeRepository;
    public ProcessTypeService(IRepository<ProcessType, int> processTypeRepository) 
    {
        _processTypeRepository = processTypeRepository;
    }
    
    public async Task<ProcessTypeDto> GetAsync(int id)
    {
        return ObjectMapper.Map<ProcessType, ProcessTypeDto>(await _processTypeRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<ProcessTypeDto>> GetListAsync()
    {
        //Get all entities
        var responseList = ObjectMapper.Map<List<ProcessType>, List<ProcessTypeDto>>(await _processTypeRepository.GetListAsync());
        var totalCount = await _processTypeRepository.CountAsync();//item count
        return new PagedResultDto<ProcessTypeDto>(totalCount,responseList);
    }

    public async Task<ProcessTypeDto> CreateAsync(SaveProcessTypeDto processType)
    {
        var entity = ObjectMapper.Map<SaveProcessTypeDto, ProcessType>(processType);
        await _processTypeRepository.InsertAsync(entity);
        return ObjectMapper.Map<ProcessType, ProcessTypeDto>(entity);
    }

    public async Task<ProcessTypeDto> UpdateAsync(int id, SaveProcessTypeDto processType)
    {
        var entity = await _processTypeRepository.GetAsync(id);
        ObjectMapper.Map(processType, entity);
        return ObjectMapper.Map<ProcessType,ProcessTypeDto>( await _processTypeRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _processTypeRepository.DeleteAsync(id);
    }
}