using System;
using HTS.Data.Entity;
using HTS.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Dto.Currency;
using HTS.Dto.ExchangeRateInformation;
using HTS.Dto.Gender;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq;

namespace HTS.Service
{
    [Authorize]
    public class ExchangeRateInformationService : ApplicationService //, IExchangeRateInformationService
    {
        private readonly IRepository<ExchangeRateInformation, int> _exchangeRateInformationRepository;

        public ExchangeRateInformationService(
            IRepository<ExchangeRateInformation, int> exchangeRateInformationRepository)
        {
            _exchangeRateInformationRepository = exchangeRateInformationRepository;
        }

        public async Task<ExchangeRateInformationDto> GetAsync(int currencyId)
        {
            var result = (await _exchangeRateInformationRepository.GetListAsync(e => e.CurrencyId == currencyId))
                            .OrderByDescending(e => e.CreationTime.Date)
                            .FirstOrDefault();

            return ObjectMapper.Map<ExchangeRateInformation, ExchangeRateInformationDto>(result);
        }
    }
}