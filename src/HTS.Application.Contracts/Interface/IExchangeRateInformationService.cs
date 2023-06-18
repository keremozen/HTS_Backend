using System.Threading.Tasks;
using HTS.Dto.ExchangeRateInformation;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IExchangeRateInformationService : IApplicationService
    {
        Task<ExchangeRateInformationDto> GetAsync(int currencyId);
    }
}
