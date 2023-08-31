using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalStaff;
using HTS.Dto.HospitalUHBStaff;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace HTS.Service;
[Authorize("HTS.HospitalManagement")]
public class HospitalUHBStaffService : ApplicationService, IHospitalUHBStaffService
{
    private readonly IRepository<HospitalUHBStaff, int> _hospitalUHBStaffRepository;
    public HospitalUHBStaffService(IRepository<HospitalUHBStaff, int> hospitalUHBStaffRepository)
    {
        _hospitalUHBStaffRepository = hospitalUHBStaffRepository;
    }

    public async Task<PagedResultDto<HospitalUHBStaffDto>> GetByHospitalListAsync(int hospitalId)
    {
        //Get all entities
        var query = (await _hospitalUHBStaffRepository.GetQueryableAsync())
            .Where(s => s.HospitalId == hospitalId);
        var totalCount = await _hospitalUHBStaffRepository.CountAsync();//item count
        var responseList = ObjectMapper.Map<List<HospitalUHBStaff>, List<HospitalUHBStaffDto>>(await AsyncExecuter.ToListAsync(query));
        return new PagedResultDto<HospitalUHBStaffDto>(totalCount, responseList);
    }

    public async Task CreateAsync(SaveHospitalUHBStaffDto hospitalStaff)
    {
        var entity = ObjectMapper.Map<SaveHospitalUHBStaffDto, HospitalUHBStaff>(hospitalStaff);
        await _hospitalUHBStaffRepository.InsertAsync(entity);

    }

    public async Task UpdateAsync(int id, SaveHospitalUHBStaffDto hospitalStaff)
    {
        var entity = await _hospitalUHBStaffRepository.GetAsync(id);
        ObjectMapper.Map(hospitalStaff, entity);
        await _hospitalUHBStaffRepository.UpdateAsync(entity);
    }


    public async Task DeleteAsync(int id)
    {
        await _hospitalUHBStaffRepository.DeleteAsync(id);
    }

}