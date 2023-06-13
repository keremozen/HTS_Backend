using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.HospitalResponseType;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
[Authorize]
public class HospitalResponseTypeService : ApplicationService, IHospitalResponseTypeService
{
    private readonly IRepository<HospitalResponseType, int> _hospitalResponseTypeRepository;
    public HospitalResponseTypeService(IRepository<HospitalResponseType, int> hospitalResponseTypeRepository) 
    {
        _hospitalResponseTypeRepository = hospitalResponseTypeRepository;
    }
    
    public async Task<ListResultDto<HospitalResponseTypeDto>> GetListAsync()
    {
        var responseList = ObjectMapper.Map<List<HospitalResponseType>, List<HospitalResponseTypeDto>>(await _hospitalResponseTypeRepository.GetListAsync());
        //Return the result
        return new ListResultDto<HospitalResponseTypeDto>(responseList);
    }
    
}