using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ProcessType;
using HTS.Dto.RejectReason;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
[Authorize]
public class RejectReasonService : ApplicationService, IRejectReasonService
{
    private readonly IRepository<RejectReason, int> _rejectReasonRepository;
    public RejectReasonService(IRepository<RejectReason, int> rejectReasonRepository) 
    {
        _rejectReasonRepository = rejectReasonRepository;
    }
    
    public async Task<RejectReasonDto> GetAsync(int id)
    {
        return ObjectMapper.Map<RejectReason, RejectReasonDto>(await _rejectReasonRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<RejectReasonDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _rejectReasonRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<RejectReason>, List<RejectReasonDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _rejectReasonRepository.CountAsync();//item count
        return new PagedResultDto<RejectReasonDto>(totalCount,responseList);
    }

    public async Task<RejectReasonDto> CreateAsync(SaveRejectReasonDto rejectReason)
    {
        var entity = ObjectMapper.Map<SaveRejectReasonDto, RejectReason>(rejectReason);
        await _rejectReasonRepository.InsertAsync(entity);
        return ObjectMapper.Map<RejectReason, RejectReasonDto>(entity);
    }

    public async Task<RejectReasonDto> UpdateAsync(int id, SaveRejectReasonDto rejectReason)
    {
        var entity = await _rejectReasonRepository.GetAsync(id);
        ObjectMapper.Map(rejectReason, entity);
        return ObjectMapper.Map<RejectReason,RejectReasonDto>( await _rejectReasonRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _rejectReasonRepository.DeleteAsync(id);
    }
}