using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Nationality;
using HTS.Dto.PaymentReason;
using HTS.Dto.ProcessKind;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class ProcessKindService : ApplicationService, IProcessKindService
{
    private readonly IRepository<ProcessKind, int> _pkRepository;
    public ProcessKindService(IRepository<ProcessKind, int> pkRepository) 
    {
        _pkRepository = pkRepository;
    }
    
    public async Task<ProcessKindDto> GetAsync(int id)
    {
        return ObjectMapper.Map<ProcessKind, ProcessKindDto>(await _pkRepository.GetAsync(id));
    }

    public async Task<ListResultDto<ProcessKindDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _pkRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            n => n.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<ProcessKind>, List<ProcessKindDto>>(await AsyncExecuter.ToListAsync(query));
        return new ListResultDto<ProcessKindDto>(responseList);
    }

    [Authorize("HTS.ProcessKindManagement")]
    public async Task CreateAsync(SaveProcessKindDto processKind)
    {
        var entity = ObjectMapper.Map<SaveProcessKindDto, ProcessKind>(processKind);
        await _pkRepository.InsertAsync(entity);
    }

    [Authorize("HTS.ProcessKindManagement")]
    public async Task UpdateAsync(int id, SaveProcessKindDto processKind)
    {
        var entity = await _pkRepository.GetAsync(id);
        ObjectMapper.Map(processKind, entity);
        await _pkRepository.UpdateAsync(entity);
    }

    [Authorize("HTS.ProcessKindManagement")]
    public async Task DeleteAsync(int id)
    {
        await _pkRepository.DeleteAsync(id);
    }
}