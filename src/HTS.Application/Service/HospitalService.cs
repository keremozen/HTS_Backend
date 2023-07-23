using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Hospital;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
public class HospitalService : ApplicationService, IHospitalService
{
    private readonly IRepository<Hospital, int> _hospitalRepository;
    public HospitalService(IRepository<Hospital, int> hospitalRepository) 
    {
        _hospitalRepository = hospitalRepository;
    }
    
    public async Task<HospitalDto> GetAsync(int id)
    {
        var query = (await _hospitalRepository.WithDetailsAsync()).Where(p => p.Id == id);
        return ObjectMapper.Map<Hospital, HospitalDto>(await AsyncExecuter.FirstOrDefaultAsync(query));
    }

    public async Task<PagedResultDto<HospitalDto>> GetListAsync(bool? isActive=null)
    {
        //Get all entities
        var query = await _hospitalRepository.WithDetailsAsync();
        query = query.WhereIf(isActive.HasValue,
            b => b.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<Hospital>, List<HospitalDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _hospitalRepository.CountAsync();//item count
        return new PagedResultDto<HospitalDto>(totalCount,responseList);
    }

    [Authorize]
    public async Task<HospitalDto> CreateAsync(SaveHospitalDto hospital)
    {
        var entity = ObjectMapper.Map<SaveHospitalDto, Hospital>(hospital);
        await _hospitalRepository.InsertAsync(entity);
        return ObjectMapper.Map<Hospital, HospitalDto>(entity);
    }

    [Authorize]
    public async Task<HospitalDto> UpdateAsync(int id, SaveHospitalDto hospital)
    {
        var entity = await _hospitalRepository.GetAsync(id);
        ObjectMapper.Map(hospital, entity);
        return ObjectMapper.Map<Hospital,HospitalDto>( await _hospitalRepository.UpdateAsync(entity));
    }

    [Authorize]
    public async Task DeleteAsync(int id)
    {
        await _hospitalRepository.DeleteAsync(id);
    }
}