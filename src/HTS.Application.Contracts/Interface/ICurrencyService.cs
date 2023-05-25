using System.Threading.Tasks;
using HTS.Dto.Currency;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface ICurrencyService : IApplicationService
    {
        Task<ListResultDto<CurrencyDto>> GetListAsync();
    }
}
