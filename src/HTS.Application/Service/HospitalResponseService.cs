using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.HospitalResponse;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class HospitalResponseService : ApplicationService, IHospitalResponseService
{
    private readonly IRepository<HospitalResponse, int> _hospitalResponseRepository;
    public HospitalResponseService(IRepository<HospitalResponse, int> hospitalResponseRepository) 
    {
        _hospitalResponseRepository = hospitalResponseRepository;
    }
    
    public async Task<HospitalResponseDto> GetAsync(int id)
    {
        return ObjectMapper.Map<HospitalResponse, HospitalResponseDto>(await _hospitalResponseRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<HospitalResponseDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _hospitalResponseRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<HospitalResponse>, List<HospitalResponseDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _hospitalResponseRepository.CountAsync();//item count
        return new PagedResultDto<HospitalResponseDto>(totalCount,responseList);
    }

    public async Task<HospitalResponseDto> CreateAsync(SaveHospitalResponseDto hospitalResponse)
    {
        var entity = ObjectMapper.Map<SaveHospitalResponseDto, HospitalResponse>(hospitalResponse);
        await _hospitalResponseRepository.InsertAsync(entity);
        return ObjectMapper.Map<HospitalResponse, HospitalResponseDto>(entity);
    }

    public async Task<HospitalResponseDto> UpdateAsync(int id, SaveHospitalResponseDto hospitalResponse)
    {
        var entity = await _hospitalResponseRepository.GetAsync(id);
        ObjectMapper.Map(hospitalResponse, entity);
        return ObjectMapper.Map<HospitalResponse,HospitalResponseDto>( await _hospitalResponseRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _hospitalResponseRepository.DeleteAsync(id);
    }
}