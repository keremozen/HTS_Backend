using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.HospitalStaff;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace HTS.Service;

public class HospitalStaffService : ApplicationService, IHospitalStaffService
{
    private readonly IRepository<HospitalStaff, int> _hospitalStaffRepository;
    private IIdentityUserRepository _userRepository;
    public HospitalStaffService(IRepository<HospitalStaff, int> hospitalStaffRepository, IIdentityUserRepository userRepository)
    {
        _hospitalStaffRepository = hospitalStaffRepository;
        _userRepository = userRepository;  
    }

    public async Task<PagedResultDto<HospitalStaffDto>> GetByInstitutionListAsync(int hospitalId)
    {
        //Get all entities
        var query = (await _hospitalStaffRepository.GetQueryableAsync()).Where(s => s.HospitalId == hospitalId);
        var totalCount = await _hospitalStaffRepository.CountAsync();//item count

        var staffList = await AsyncExecuter.ToListAsync(query);

        Dictionary<Guid, IdentityUserDto> identityUsers = new Dictionary<Guid, IdentityUserDto>();
        List<HospitalStaffDto> result = new List<HospitalStaffDto>();
        foreach (var staff in staffList)
        {
            HospitalStaffDto staffDto = ObjectMapper.Map<HospitalStaff, HospitalStaffDto>(staff);
            //Set user information
            if (identityUsers.ContainsKey(staff.UserId))//Already exist
            {
                staffDto.User = identityUsers[staff.UserId];
            }
            else
            {//Get creator from db
                var user = ObjectMapper.Map<IdentityUser, IdentityUserDto>(await _userRepository.FindAsync(staff.UserId));
                staffDto.User = user;
                identityUsers.Add(staff.UserId, staffDto.User);
            }
            result.Add(staffDto);
        }

        return new PagedResultDto<HospitalStaffDto>(totalCount, result);
    }

    public async Task SaveAsync(int hospitalId, List<SaveHospitalStaffDto> hospitalStaffs)
    {
        var newEntities = ObjectMapper.Map<List<SaveHospitalStaffDto>, List<HospitalStaff>>(hospitalStaffs);
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