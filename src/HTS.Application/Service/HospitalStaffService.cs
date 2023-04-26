using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalStaff;
using HTS.Interface;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

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

    public async Task CreateAsync(SaveHospitalStaffDto hospitalStaff)
    {
        await CheckDefaultStaffValue(hospitalStaff);
        var entity = ObjectMapper.Map<SaveHospitalStaffDto, HospitalStaff>(hospitalStaff);
        entity.IsDefault = entity.IsActive && entity.IsDefault;//If record is passive, set as not default
        await _hospitalStaffRepository.InsertAsync(entity);

    }

    public async Task UpdateAsync(int id, SaveHospitalStaffDto hospitalStaff)
    {
        await CheckDefaultStaffValue(hospitalStaff, id);
        var entity = await _hospitalStaffRepository.GetAsync(id);
        entity.IsDefault = entity.IsActive && entity.IsDefault;//If record is passive, set as not default
        ObjectMapper.Map(hospitalStaff, entity);
        await _hospitalStaffRepository.UpdateAsync(entity);
    }


    public async Task DeleteAsync(int id)
    {
        await _hospitalStaffRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Checks default staff value. There should be only one default active staff in database
    /// </summary>
    /// <param name="hospitalStaff">Hospital staff to check default value</param>
    /// <param name="id">If hospital staff is already in database, id field should be entered</param>
    /// <exception cref="DefaultStaffAlreadyExistException"></exception>
    private async Task CheckDefaultStaffValue(SaveHospitalStaffDto hospitalStaff, int? id = null)
    {
        //check whether user exists even if it is active or not, default or not
        if (!id.HasValue && (await _hospitalStaffRepository.GetQueryableAsync()).Any(s=>s.UserId == hospitalStaff.UserId))
        {
            throw new HTSBusinessException(ErrorCode.StaffAlreadyExist);
        }
        if (hospitalStaff.IsDefault)//Default staff
        {
            if ((await _hospitalStaffRepository.GetQueryableAsync()).Any(s => s.IsActive
                                                                             && s.IsDefault
                                                                             && (!id.HasValue || s.Id != id)))
            {//There is already in db
                throw new HTSBusinessException(ErrorCode.DefaultStaffAlreadyExist);
            }
        }
    }

}