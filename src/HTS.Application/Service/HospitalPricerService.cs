using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalPricer;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace HTS.Service;
[Authorize("HTS.HospitalManagement")]
public class HospitalPricerService : ApplicationService, IHospitalPricerService
{
    private readonly IRepository<HospitalPricer, int> _hospitalPricerRepository;
    private IIdentityUserRepository _userRepository;
    public HospitalPricerService(IRepository<HospitalPricer, int> hospitalPricerRepository, IIdentityUserRepository userRepository)
    {
        _hospitalPricerRepository = hospitalPricerRepository;
        _userRepository = userRepository;
    }

    public async Task<PagedResultDto<HospitalPricerDto>> GetByHospitalListAsync(int hospitalId,bool? isActive=null)
    {
        //Get all entities
        var query = (await _hospitalPricerRepository.GetQueryableAsync())
            .Where(s => s.HospitalId == hospitalId)
            .WhereIf(isActive.HasValue,
                s => s.IsActive == isActive.Value);;
        var totalCount = await _hospitalPricerRepository.CountAsync();//item count
        var responseList = ObjectMapper.Map<List<HospitalPricer>, List<HospitalPricerDto>>(await AsyncExecuter.ToListAsync(query));
        return new PagedResultDto<HospitalPricerDto>(totalCount, responseList);
    }

    public async Task CreateAsync(SaveHospitalPricerDto hospitalPricer)
    {
        await IsDataValidToSave(hospitalPricer);
        var entity = ObjectMapper.Map<SaveHospitalPricerDto, HospitalPricer>(hospitalPricer);
        entity.IsDefault = entity.IsActive && entity.IsDefault;//If record is passive, set as not default
        await _hospitalPricerRepository.InsertAsync(entity);

    }

    public async Task UpdateAsync(int id, SaveHospitalPricerDto hospitalPricer)
    {
        await IsDataValidToSave(hospitalPricer, id);
        var entity = await _hospitalPricerRepository.GetAsync(id);
        ObjectMapper.Map(hospitalPricer, entity);
        entity.IsDefault = entity.IsActive && entity.IsDefault;//If record is passive, set as not default
        await _hospitalPricerRepository.UpdateAsync(entity);
    }


    public async Task DeleteAsync(int id)
    {
        await _hospitalPricerRepository.DeleteAsync(id);
    }



    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="hospitalPricer">To be saved object</param>
    /// <param name="id">Id in updated entity</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async Task IsDataValidToSave(SaveHospitalPricerDto hospitalPricer, int? id = null)
    {

        if (!id.HasValue //Insert
            && (await _hospitalPricerRepository.GetQueryableAsync()).Any(s => s.UserId == hospitalPricer.UserId && s.HospitalId == hospitalPricer.HospitalId))
        {//UserId already added
            throw new HTSBusinessException(ErrorCode.PricerAlreadyExist);
        }
        if (hospitalPricer.IsDefault)//Default pricer
        {
            if ((await _hospitalPricerRepository.GetQueryableAsync()).Any(s => s.IsActive
                                                                             && s.IsDefault
                                                                             && (!id.HasValue || s.Id != id)
                                                                             && s.HospitalId == hospitalPricer.HospitalId))
            {//There is already default pricer in db
                throw new HTSBusinessException(ErrorCode.DefaultPricerAlreadyExist);
            }
        }
    }

}