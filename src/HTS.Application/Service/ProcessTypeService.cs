using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ProcessType;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
[Authorize]
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

    public async Task<PagedResultDto<ProcessTypeDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _processTypeRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<ProcessType>, List<ProcessTypeDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _processTypeRepository.CountAsync();//item count
        return new PagedResultDto<ProcessTypeDto>(totalCount,responseList);
    }

}