using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Branch;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class BranchService : ApplicationService, IBranchService
{
    private readonly IRepository<Branch, int> _branchRepository;
    public BranchService(IRepository<Branch, int> branchRepository) 
    {
        _branchRepository = branchRepository;
    }
    
    public async Task<BranchDto> GetAsync(int id)
    {
        return ObjectMapper.Map<Branch, BranchDto>(await _branchRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<BranchDto>> GetListAsync()
    {
        //Get all entities
        var responseList = ObjectMapper.Map<List<Branch>, List<BranchDto>>(await _branchRepository.GetListAsync());
        var totalCount = await _branchRepository.CountAsync();//item count
        return new PagedResultDto<BranchDto>(totalCount,responseList);
    }

    public async Task<BranchDto> CreateAsync(SaveBranchDto major)
    {
        var entity = ObjectMapper.Map<SaveBranchDto, Branch>(major);
        await _branchRepository.InsertAsync(entity);
        return ObjectMapper.Map<Branch, BranchDto>(entity);
    }

    public async Task<BranchDto> UpdateAsync(int id, SaveBranchDto major)
    {
        var entity = await _branchRepository.GetAsync(id);
        ObjectMapper.Map(major, entity);
        return ObjectMapper.Map<Branch, BranchDto>( await _branchRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _branchRepository.DeleteAsync(id);
    }
}