using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.HospitalizationType;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class HospitalizationTypeService : ApplicationService, IHospitalizationTypeService
{
    private readonly IRepository<HospitalizationType, int> _hospitalizationTypeRepository;
    public HospitalizationTypeService(IRepository<HospitalizationType, int> hospitalizationTypeRepository) 
    {
        _hospitalizationTypeRepository = hospitalizationTypeRepository;
    }

    public async Task<PagedResultDto<HospitalizationTypeDto>> GetListAsync()
    {
        //Get all entities
        var query = await _hospitalizationTypeRepository.GetQueryableAsync();
        var responseList = ObjectMapper.Map<List<HospitalizationType>, List<HospitalizationTypeDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _hospitalizationTypeRepository.CountAsync();//item count
        return new PagedResultDto<HospitalizationTypeDto>(totalCount,responseList);
    }

  
}