using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using HTS.Dto.ContractedInstitution;

namespace HTS.Interface
{
    public interface IContractedInstitutionService : IApplicationService
    {
        /// <summary>
        /// Get contracted institution by id
        /// </summary>
        /// <param name="id">Desired contracted institution id</param>
        /// <returns>Desired contracted institution</returns>
        Task<ContractedInstitutionDto> GetAsync(int id);
        /// <summary>
        /// Get all contracted institutions
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Contracted institution list</returns>
        Task<PagedResultDto<ContractedInstitutionDto>> GetListAsync(bool? isActive=null);
        /// <summary>
        /// Creates contracted institution
        /// </summary>
        /// <param name="contractedInstitution">contracted institution information to be insert</param>
        /// <returns>Inserted nationality</returns>
        Task<ContractedInstitutionDto> CreateAsync(SaveContractedInstitutionDto contractedInstitution);
        
        /// <summary>
        /// Updates contracted institution
        /// </summary>
        /// <param name="id">To be updated contracted institution id</param>
        /// <param name="contractedInstitution">To be updated information</param>
        /// <returns>Updated contracted institution object</returns>
        Task<ContractedInstitutionDto> UpdateAsync(int id, SaveContractedInstitutionDto contractedInstitution);
        
        /// <summary>
        /// Delete given id of contracted institution
        /// </summary>
        /// <param name="id">To be deleted contracted institution id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
