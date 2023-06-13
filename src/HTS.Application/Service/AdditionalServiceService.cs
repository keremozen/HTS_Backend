using HTS.Data.Entity;
using HTS.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Dto.AdditionalService;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    [Authorize]
    public class AdditionalServiceService : ApplicationService, IAdditionalServiceService
    {
        private readonly IRepository<AdditionalService, int> _additionalServiceRepository;
        public AdditionalServiceService(IRepository<AdditionalService, int> additionalServiceRepository)
        {
            _additionalServiceRepository = additionalServiceRepository;

        }
        public async Task<ListResultDto<AdditionalServiceDto>> GetListAsync()
        {
            var responseList = ObjectMapper.Map<List<AdditionalService>, List<AdditionalServiceDto>>(await _additionalServiceRepository.GetListAsync());
            //Return the result
            return new ListResultDto<AdditionalServiceDto>(responseList);
        }
    }
}
