using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ContractedInstitution;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace HTS.Service;

public class ContractedInstitutionService : ApplicationService, IContractedInstitutionService
{
    private readonly IRepository<ContractedInstitution, int> _contractedInstitutionRepository;
    public ContractedInstitutionService(IRepository<ContractedInstitution, int> contractedInstitutionRepository) 
    {
        _contractedInstitutionRepository = contractedInstitutionRepository;
    }
    
    public async Task<ContractedInstitutionDto> GetAsync(int id)
    {
        var query = (await _contractedInstitutionRepository.WithDetailsAsync()).Where(p => p.Id == id);
        return ObjectMapper.Map<ContractedInstitution, ContractedInstitutionDto>(await AsyncExecuter.FirstOrDefaultAsync(query));
    }
    
    public async Task<PagedResultDto<ContractedInstitutionDto>> GetListAsync()
    {
        //Get all entities
        var query = await _contractedInstitutionRepository.WithDetailsAsync();
        var responseList = ObjectMapper.Map<List<ContractedInstitution>, List<ContractedInstitutionDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _contractedInstitutionRepository.CountAsync();//item count
        //TODO:Hopsy Ask Kerem the isActive case 
        return new PagedResultDto<ContractedInstitutionDto>(totalCount,responseList);
    }

    public async Task<ContractedInstitutionDto> CreateAsync(SaveContractedInstitutionDto contractedInstitution)
    {
        var entity = ObjectMapper.Map<SaveContractedInstitutionDto, ContractedInstitution>(contractedInstitution);
        await _contractedInstitutionRepository.InsertAsync(entity);
        return ObjectMapper.Map<ContractedInstitution, ContractedInstitutionDto>(entity);
    }

    public async Task<ContractedInstitutionDto> UpdateAsync(int id, SaveContractedInstitutionDto contractedInstitution)
    {
        var entity = await _contractedInstitutionRepository.GetAsync(id);
        ObjectMapper.Map(contractedInstitution, entity);
        return ObjectMapper.Map<ContractedInstitution, ContractedInstitutionDto>( await _contractedInstitutionRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _contractedInstitutionRepository.DeleteAsync(id);
    }
}