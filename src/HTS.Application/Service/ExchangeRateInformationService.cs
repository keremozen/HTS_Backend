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
            var result =
                await _exchangeRateInformationRepository.FirstOrDefaultAsync(e =>
                    e.CreationTime.Date.Date == DateTime.Now.Date.AddDays(-1));
            return ObjectMapper.Map<ExchangeRateInformation, ExchangeRateInformationDto>(result);
        }
    }
}