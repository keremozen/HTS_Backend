using System.Threading.Tasks;
using HTS.Dto.HospitalResponseType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IHospitalResponseTypeService : IApplicationService
    {
        /// <summary>
        /// Gets hospital response type item list. Predefined data. All has enum values
        /// </summary>
        /// <returns>Hospital response type list</returns>
        Task<ListResultDto<HospitalResponseTypeDto>> GetListAsync();
    }
}
