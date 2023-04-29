using System.Threading.Tasks;
using HTS.Dto.ProcessType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IProcessTypeService : IApplicationService
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired entity</returns>
        Task<ProcessTypeDto> GetAsync(int id);
        /// <summary>
        /// Get all process types
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Process type list</returns>
        Task<PagedResultDto<ProcessTypeDto>> GetListAsync(bool? isActive=null);
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="processType">ProcessType information to be insert</param>
        /// <returns>Inserted entity object</returns>
        Task<ProcessTypeDto> CreateAsync(SaveProcessTypeDto processType);
        
        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="processType">To be updated information</param>
        /// <returns>Updated object</returns>
        Task<ProcessTypeDto> UpdateAsync(int id, SaveProcessTypeDto processType);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
