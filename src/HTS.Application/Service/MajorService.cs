using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Major;
using HTS.Dto.Language;
using HTS.Dto.Major;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class MajorService : ApplicationService, IMajorService
{
    private readonly IRepository<Major, int> _majorRepository;
    public MajorService(IRepository<Major, int> majorRepository) 
    {
        _majorRepository = majorRepository;
    }
    
    public async Task<MajorDto> GetAsync(int id)
    {
        return ObjectMapper.Map<Major, MajorDto>(await _majorRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<MajorDto>> GetListAsync()
    {
        //Get all entities
        var responseList = ObjectMapper.Map<List<Major>, List<MajorDto>>(await _majorRepository.GetListAsync());
        var totalCount = await _majorRepository.CountAsync();//item count
        return new PagedResultDto<MajorDto>(totalCount,responseList);
    }

    public async Task<MajorDto> CreateAsync(SaveMajorDto major)
    {
        var entity = ObjectMapper.Map<SaveMajorDto, Major>(major);
        await _majorRepository.InsertAsync(entity);
        return ObjectMapper.Map<Major, MajorDto>(entity);
    }

    public async Task<MajorDto> UpdateAsync(int id, SaveMajorDto major)
    {
        var entity = await _majorRepository.GetAsync(id);
        ObjectMapper.Map(major, entity);
        return ObjectMapper.Map<Major,MajorDto>( await _majorRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _majorRepository.DeleteAsync(id);
    }
}