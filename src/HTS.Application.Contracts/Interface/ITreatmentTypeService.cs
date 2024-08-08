using System.Threading.Tasks;
using HTS.Dto.TreatmentType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface ITreatmentTypeService : IApplicationService
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired entity</returns>
        Task<TreatmentTypeDto> GetAsync(int id);
        /// <summary>
        /// Get all treatment types
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Treatment type list</returns>
        Task<PagedResultDto<TreatmentTypeDto>> GetListAsync(bool? isActive=null);
        
    }
}
