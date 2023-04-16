using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ProcessRelation;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class ProcessRelationService : ApplicationService, IProcessRelationService
{
    private readonly IRepository<ProcessRelation, int> _processRelationRepository;
    public ProcessRelationService(IRepository<ProcessRelation, int> processRelationRepository) 
    {
        _processRelationRepository = processRelationRepository;
    }
    
    public async Task<ProcessRelationDto> GetAsync(int id)
    {
        return ObjectMapper.Map<ProcessRelation, ProcessRelationDto>(await _processRelationRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<ProcessRelationDto>> GetListAsync()
    {
        //Get all entities
        var responseList = ObjectMapper.Map<List<ProcessRelation>, List<ProcessRelationDto>>(await _processRelationRepository.GetListAsync());
        var totalCount = await _processRelationRepository.CountAsync();//item count
        return new PagedResultDto<ProcessRelationDto>(totalCount,responseList);
    }

    public async Task<ProcessRelationDto> CreateAsync(SaveProcessRelationDto processRelation)
    {
        var entity = ObjectMapper.Map<SaveProcessRelationDto, ProcessRelation>(processRelation);
        await _processRelationRepository.InsertAsync(entity);
        return ObjectMapper.Map<ProcessRelation, ProcessRelationDto>(entity);
    }

    public async Task<ProcessRelationDto> UpdateAsync(int id, SaveProcessRelationDto processRelation)
    {
        var entity = await _processRelationRepository.GetAsync(id);
        ObjectMapper.Map(processRelation, entity);
        return ObjectMapper.Map<ProcessRelation,ProcessRelationDto>( await _processRelationRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _processRelationRepository.DeleteAsync(id);
    }
}