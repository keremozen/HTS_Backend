using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ContractedInstitution;
using HTS.Dto.ContractedInstitutionStaff;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class ContractedInstitutionStaffService : ApplicationService, IContractedInstitutionStaffService
{
    private readonly IRepository<ContractedInstitutionStaff, int> _contractedInstitutionStaffRepository;
    public ContractedInstitutionStaffService(IRepository<ContractedInstitutionStaff, int> contractedInstitutionStaffRepository) 
    {
        _contractedInstitutionStaffRepository = contractedInstitutionStaffRepository;
    }

    public async Task<PagedResultDto<ContractedInstitutionStaffDto>> GetByInstitutionListAsync(int institutionId)
    {
        //Get all entities
         var query = (await _contractedInstitutionStaffRepository.GetQueryableAsync()).Where(p => p.ContractedInstitutionId == institutionId);
         var responseList = ObjectMapper.Map<List<ContractedInstitutionStaff>, List<ContractedInstitutionStaffDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _contractedInstitutionStaffRepository.CountAsync();//item count
        return new PagedResultDto<ContractedInstitutionStaffDto>(totalCount,responseList);
    }

    public async Task SaveAsync(int institutionId, List<SaveContractedInstitutionStaffDto> contractedInstitutionStaffs)
    {
        var newEntities = ObjectMapper.Map<List<SaveContractedInstitutionStaffDto>,List<ContractedInstitutionStaff>>(contractedInstitutionStaffs);
        var query = (await _contractedInstitutionStaffRepository.GetQueryableAsync())
            .Where(p => p.ContractedInstitutionId == institutionId);
        var dbEntities = await AsyncExecuter.ToListAsync(query);
        await _contractedInstitutionStaffRepository.DeleteManyAsync(dbEntities);
        await _contractedInstitutionStaffRepository.InsertManyAsync(newEntities);
    }

    public async Task DeleteAsync(int id)
    {
        await _contractedInstitutionStaffRepository.DeleteAsync(id);
    }
}