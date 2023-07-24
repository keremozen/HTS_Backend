using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ContractedInstitutionKind;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
public class ContractedInstitutionKindService : ApplicationService, IContractedInstitutionKindService
{
    private readonly IRepository<ContractedInstitutionKind, int> _ciKindRepository;
    public ContractedInstitutionKindService(IRepository<ContractedInstitutionKind, int> ciKindRepository) 
    {
        _ciKindRepository = ciKindRepository;
    }
    
    public async Task<ContractedInstitutionKindDto> GetAsync(int id)
    {
        return ObjectMapper.Map<ContractedInstitutionKind, ContractedInstitutionKindDto>(await _ciKindRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<ContractedInstitutionKindDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _ciKindRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            n => n.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<ContractedInstitutionKind>, List<ContractedInstitutionKindDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _ciKindRepository.CountAsync();//item count
        return new PagedResultDto<ContractedInstitutionKindDto>(totalCount,responseList);
    }

    [Authorize]
    public async Task CreateAsync(SaveContractedInstitutionKindDto type)
    {
        var entity = ObjectMapper.Map<SaveContractedInstitutionKindDto, ContractedInstitutionKind>(type);
        await _ciKindRepository.InsertAsync(entity);
    }

    [Authorize]
    public async Task UpdateAsync(int id, SaveContractedInstitutionKindDto type)
    {
        var entity = await _ciKindRepository.GetAsync(id);
        ObjectMapper.Map(type, entity); 
        await _ciKindRepository.UpdateAsync(entity);
    }

    [Authorize]
    public async Task DeleteAsync(int id)
    {
        await _ciKindRepository.DeleteAsync(id);
    }
}