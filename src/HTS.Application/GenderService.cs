using HTS.Data.Entity;
using HTS.Dto;
using HTS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace HTS
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
            var responseList = ObjectMapper.Map<List<Gender>,List<GenderDto>>(await _genderRepository.GetListAsync());
            //Return the result
            return new ListResultDto<GenderDto>(responseList);
        }
    }
}
