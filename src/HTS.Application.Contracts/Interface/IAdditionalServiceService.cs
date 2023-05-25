using System.Threading.Tasks;
using HTS.Dto.AdditionalService;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IAdditionalServiceService : IApplicationService
    {
        Task<ListResultDto<AdditionalServiceDto>> GetListAsync();
    }
}
