using System.Threading.Tasks;
using HTS.Dto.ProcessRelation;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IProcessRelationService : IApplicationService
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired entity</returns>
        Task<ProcessRelationDto> GetAsync(int id);
        /// <summary>
        /// Get all process relations
        /// </summary>
        /// <returns>Object list</returns>
        Task<PagedResultDto<ProcessRelationDto>> GetListAsync();
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="processRelation">ProcessRelation information to be insert</param>
        /// <returns>Inserted entity object</returns>
        Task<ProcessRelationDto> CreateAsync(SaveProcessRelationDto processRelation);
        
        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="processRelation">To be updated information</param>
        /// <returns>Updated object</returns>
        Task<ProcessRelationDto> UpdateAsync(int id, SaveProcessRelationDto processRelation);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
