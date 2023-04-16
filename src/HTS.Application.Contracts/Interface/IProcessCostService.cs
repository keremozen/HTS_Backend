using System.Threading.Tasks;
using HTS.Dto.ProcessCost;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IProcessCostService : IApplicationService
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired entity</returns>
        Task<ProcessCostDto> GetAsync(int id);
        /// <summary>
        /// Get all process costs
        /// </summary>
        /// <returns>Object list</returns>
        Task<PagedResultDto<ProcessCostDto>> GetListAsync();
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="processCost">ProcessCost information to be insert</param>
        /// <returns>Inserted entity object</returns>
        Task<ProcessCostDto> CreateAsync(SaveProcessCostDto processCost);
        
        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="processCost">To be updated information</param>
        /// <returns>Updated object</returns>
        Task<ProcessCostDto> UpdateAsync(int id, SaveProcessCostDto processCost);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
