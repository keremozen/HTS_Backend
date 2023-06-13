using HTS.Data.Entity;
using HTS.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Dto.Currency;
using HTS.Dto.Gender;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    [Authorize]
    public class CurrencyService : ApplicationService, ICurrencyService
    {
        private readonly IRepository<Currency, int> _currencyRepository;
        public CurrencyService(IRepository<Currency, int> currencyRepository)
        {
            _currencyRepository = currencyRepository;

        }
        public async Task<ListResultDto<CurrencyDto>> GetListAsync()
        {
            var responseList = ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(await _currencyRepository.GetListAsync());
            //Return the result
            return new ListResultDto<CurrencyDto>(responseList);
        }
    }
}
