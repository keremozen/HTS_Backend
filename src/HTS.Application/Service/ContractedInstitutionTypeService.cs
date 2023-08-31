using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ContractedInstitutionType;
using HTS.Dto.Nationality;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
public class ContractedInstitutionTypeService : ApplicationService, IContractedInstitutionTypeService
{
    private readonly IRepository<ContractedInstitutionType, int> _ciTypeRepository;
    public ContractedInstitutionTypeService(IRepository<ContractedInstitutionType, int> ciTypeRepository) 
    {
        _ciTypeRepository = ciTypeRepository;
    }
    
    public async Task<ContractedInstitutionTypeDto> GetAsync(int id)
    {
        return ObjectMapper.Map<ContractedInstitutionType, ContractedInstitutionTypeDto>(await _ciTypeRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<ContractedInstitutionTypeDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _ciTypeRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            n => n.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<ContractedInstitutionType>, List<ContractedInstitutionTypeDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _ciTypeRepository.CountAsync();//item count
        return new PagedResultDto<ContractedInstitutionTypeDto>(totalCount,responseList);
    }

    [Authorize("HTS.ContractedInstitutionTypeManagement")]
    public async Task CreateAsync(SaveContractedInstitutionTypeDto type)
    {
        var entity = ObjectMapper.Map<SaveContractedInstitutionTypeDto, ContractedInstitutionType>(type);
        await _ciTypeRepository.InsertAsync(entity);
    }

    [Authorize("HTS.ContractedInstitutionTypeManagement")]
    public async Task UpdateAsync(int id, SaveContractedInstitutionTypeDto type)
    {
        var entity = await _ciTypeRepository.GetAsync(id);
        ObjectMapper.Map(type, entity); 
        await _ciTypeRepository.UpdateAsync(entity);
    }

    [Authorize("HTS.ContractedInstitutionTypeManagement")]
    public async Task DeleteAsync(int id)
    {
        await _ciTypeRepository.DeleteAsync(id);
    }
}