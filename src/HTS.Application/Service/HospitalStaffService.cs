using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.ContractedInstitution;
using HTS.Dto.ContractedInstitutionStaff;
using HTS.Dto.HospitalStaff;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class HospitalStaffService : ApplicationService, IHospitalStaffService
{
    private readonly IRepository<HospitalStaff, int> _hospitalStaffRepository;
    public HospitalStaffService(IRepository<HospitalStaff, int> hospitalStaffRepository) 
    {
        _hospitalStaffRepository = hospitalStaffRepository;
    }

    public async Task<PagedResultDto<HospitalStaffDto>> GetByInstitutionListAsync(int hospitalId)
    {
        //Get all entities
         var query = (await _hospitalStaffRepository.GetQueryableAsync()).Where(s => s.HospitalId == hospitalId);
         var responseList = ObjectMapper.Map<List<HospitalStaff>, List<HospitalStaffDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _hospitalStaffRepository.CountAsync();//item count
        return new PagedResultDto<HospitalStaffDto>(totalCount,responseList);
    }

    public async Task SaveAsync(int hospitalId, List<SaveHospitalStaffDto> hospitalStaffs)
    {
        var newEntities = ObjectMapper.Map<List<SaveHospitalStaffDto>,List<HospitalStaff>>(hospitalStaffs);
        var query = (await _hospitalStaffRepository.GetQueryableAsync())
            .Where(p => p.HospitalId == hospitalId);
        var dbEntities = await AsyncExecuter.ToListAsync(query);
        await _hospitalStaffRepository.DeleteManyAsync(dbEntities);
        await _hospitalStaffRepository.InsertManyAsync(newEntities);
    }

    public async Task DeleteAsync(int id)
    {
        await _hospitalStaffRepository.DeleteAsync(id);
    }
}