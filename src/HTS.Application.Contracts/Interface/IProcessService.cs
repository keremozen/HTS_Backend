using System.Threading.Tasks;
using HTS.Dto.Process;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IProcessService : IApplicationService
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired entity</returns>
        Task<ProcessDto> GetAsync(int id);
        /// <summary>
        /// Get all processes
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Object list</returns>
        Task<PagedResultDto<ProcessDto>> GetListAsync(bool? isActive=null);
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="process">Process information to be insert</param>
        /// <returns>Inserted entity object</returns>
        Task<ProcessDto> CreateAsync(SaveProcessDto process);
        
        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="process">To be updated information</param>
        /// <returns>Updated object</returns>
        Task<ProcessDto> UpdateAsync(int id, SaveProcessDto process);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
