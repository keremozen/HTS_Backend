using HTS.Data.Entity;
using HTS.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Dto.Gender;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    public class GenderService : ApplicationService, IGenderService
    {

        private readonly IRepository<Gender, int> _genderRepository;
        public GenderService(IRepository<Gender, int> genderRepository)
        {
            _genderRepository = genderRepository;

        }
        public async Task<ListResultDto<GenderDto>> GetListAsync()
        {
            var responseList = ObjectMapper.Map<List<Gender>, List<GenderDto>>(await _genderRepository.GetListAsync());
            //Return the result
            return new ListResultDto<GenderDto>(responseList);
        }
    }
}
