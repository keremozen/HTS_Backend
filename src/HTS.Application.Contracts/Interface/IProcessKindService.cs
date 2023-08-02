using System.Threading.Tasks;
using HTS.Dto.ContractedInstitutionKind;
using HTS.Dto.Nationality;
using HTS.Dto.PaymentReason;
using HTS.Dto.ProcessKind;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IProcessKindService : IApplicationService
    {
        /// <summary>
        /// Get object by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired object</returns>
        Task<ProcessKindDto> GetAsync(int id);
        /// <summary>
        /// Get all objects
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Object list</returns>
        Task<ListResultDto<ProcessKindDto>> GetListAsync(bool? isActive=null);

        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="processKind">To be inserted object</param>
        /// <returns></returns>
        Task CreateAsync(SaveProcessKindDto processKind);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="processKind">To be updated object</param>
        /// <returns></returns>
        Task UpdateAsync(int id, SaveProcessKindDto processKind);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
