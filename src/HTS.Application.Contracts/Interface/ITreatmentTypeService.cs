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
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="treatmentType">TreatmentType information to be insert</param>
        /// <returns>Inserted treatment type</returns>
        Task<TreatmentTypeDto> CreateAsync(SaveTreatmentTypeDto treatmentType);
        
        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="treatmentType">To be updated information</param>
        /// <returns>Updated object</returns>
        Task<TreatmentTypeDto> UpdateAsync(int id, SaveTreatmentTypeDto treatmentType);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
