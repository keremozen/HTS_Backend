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
        var query = (await _contractedInstitutionStaffRepository.WithDetailsAsync()).Where(p => p.ContractedInstitutionId == institutionId);
        var responseList = ObjectMapper.Map<List<ContractedInstitutionStaff>, List<ContractedInstitutionStaffDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _contractedInstitutionStaffRepository.CountAsync();//item count
        return new PagedResultDto<ContractedInstitutionStaffDto>(totalCount, responseList);
    }

    public async Task<ContractedInstitutionStaffDto> GetAsync(int id)
    {
        var query = (await _contractedInstitutionStaffRepository.WithDetailsAsync()).Where(p => p.Id == id);
        return ObjectMapper.Map<ContractedInstitutionStaff, ContractedInstitutionStaffDto>(await AsyncExecuter.FirstOrDefaultAsync(query));
    }

    public async Task<ContractedInstitutionStaffDto> CreateAsync(SaveContractedInstitutionStaffDto contractedInstitutionStaff)
    {
        var entity = ObjectMapper.Map<SaveContractedInstitutionStaffDto, ContractedInstitutionStaff>(contractedInstitutionStaff);
        await _contractedInstitutionStaffRepository.InsertAsync(entity);
        return ObjectMapper.Map<ContractedInstitutionStaff, ContractedInstitutionStaffDto>(entity);
    }

    public async Task<ContractedInstitutionStaffDto> UpdateAsync(int id,
        SaveContractedInstitutionStaffDto contractedInstitutionStaff)
    {
        var entity = await _contractedInstitutionStaffRepository.GetAsync(id);
        ObjectMapper.Map(contractedInstitutionStaff, entity);
        return ObjectMapper.Map<ContractedInstitutionStaff, ContractedInstitutionStaffDto>(
            await _contractedInstitutionStaffRepository.UpdateAsync(entity));
    }

    public async Task DeleteAsync(int id)
    {
        await _contractedInstitutionStaffRepository.DeleteAsync(id);
    }
}