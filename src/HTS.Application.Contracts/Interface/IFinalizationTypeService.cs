using System.Threading.Tasks;
using HTS.Dto.FinalizationType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IFinalizationTypeService : IApplicationService
    {
        /// <summary>
        /// Get object by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired object</returns>
        Task<FinalizationTypeDto> GetAsync(int id);
        
        /// <summary>
        /// Get all objects
        /// </summary>
        /// <returns>Object list</returns>
        Task<ListResultDto<FinalizationTypeDto>> GetListAsync();

        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="finalizationType">To be inserted object</param>
        /// <returns></returns>
        Task CreateAsync(SaveFinalizationTypeDto finalizationType);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="finalizationType">To be updated object</param>
        /// <returns></returns>
        Task UpdateAsync(int id, SaveFinalizationTypeDto finalizationType);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}