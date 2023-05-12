using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalStaff;
using HTS.Interface;
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

    public async Task<PagedResultDto<HospitalStaffDto>> GetByHospitalListAsync(int hospitalId,bool? isActive=null)
    {
        //Get all entities
        var query = (await _hospitalStaffRepository.GetQueryableAsync())
            .Where(s => s.HospitalId == hospitalId)
            .WhereIf(isActive.HasValue,
                s => s.IsActive == isActive.Value);;
        var totalCount = await _hospitalStaffRepository.CountAsync();//item count
        var responseList = ObjectMapper.Map<List<HospitalStaff>, List<HospitalStaffDto>>(await AsyncExecuter.ToListAsync(query));
        return new PagedResultDto<HospitalStaffDto>(totalCount, responseList);
    }

    public async Task CreateAsync(SaveHospitalStaffDto hospitalStaff)
    {
        await IsDataValidToSave(hospitalStaff);
        var entity = ObjectMapper.Map<SaveHospitalStaffDto, HospitalStaff>(hospitalStaff);
        entity.IsDefault = entity.IsActive && entity.IsDefault;//If record is passive, set as not default
        await _hospitalStaffRepository.InsertAsync(entity);

    }

    public async Task UpdateAsync(int id, SaveHospitalStaffDto hospitalStaff)
    {
        await IsDataValidToSave(hospitalStaff, id);
        var entity = await _hospitalStaffRepository.GetAsync(id);
        ObjectMapper.Map(hospitalStaff, entity);
        entity.IsDefault = entity.IsActive && entity.IsDefault;//If record is passive, set as not default
        await _hospitalStaffRepository.UpdateAsync(entity);
    }


    public async Task DeleteAsync(int id)
    {
        await _hospitalStaffRepository.DeleteAsync(id);
    }



    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="hospitalStaff">To be saved object</param>
    /// <param name="id">Id in updated entity</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async Task IsDataValidToSave(SaveHospitalStaffDto hospitalStaff, int? id = null)
    {

        if (!id.HasValue //Insert
            && (await _hospitalStaffRepository.GetQueryableAsync()).Any(s => s.UserId == hospitalStaff.UserId && s.HospitalId == hospitalStaff.HospitalId))
        {//UserId already added
            throw new HTSBusinessException(ErrorCode.StaffAlreadyExist);
        }
        if (hospitalStaff.IsDefault)//Default staff
        {
            if ((await _hospitalStaffRepository.GetQueryableAsync()).Any(s => s.IsActive
                                                                             && s.IsDefault
                                                                             && (!id.HasValue || s.Id != id)
                                                                             && s.HospitalId == hospitalStaff.HospitalId))
            {//There is already default staff in db
                throw new HTSBusinessException(ErrorCode.DefaultStaffAlreadyExist);
            }
        }
    }

}