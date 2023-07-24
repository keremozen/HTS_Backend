using System.Threading.Tasks;
using HTS.Dto.ContractedInstitutionType;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IContractedInstitutionTypeService : IApplicationService
    {
        /// <summary>
        /// Get object by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired object</returns>
        Task<ContractedInstitutionTypeDto> GetAsync(int id);
        /// <summary>
        /// Get all objects
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Object list</returns>
        Task<PagedResultDto<ContractedInstitutionTypeDto>> GetListAsync(bool? isActive=null);

        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="ciType"></param>
        /// <returns></returns>
        Task CreateAsync(SaveContractedInstitutionTypeDto ciType);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="ciType"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, SaveContractedInstitutionTypeDto ciType);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
