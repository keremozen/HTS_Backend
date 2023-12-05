using System.Threading.Tasks;
using HTS.Dto.TreatmentProcessStatus;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface ITreatmentProcessStatusService : IApplicationService
    {
        /// <summary>
        /// Gets treatment process status list. Predefined data. All has enum values
        /// </summary>
        /// <returns>Treatment process status list</returns>
        Task<ListResultDto<TreatmentProcessStatusDto>> GetListAsync();
    }
}
