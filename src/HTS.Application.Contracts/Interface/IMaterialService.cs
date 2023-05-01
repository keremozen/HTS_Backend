using HTS.Dto.Branch;
using System.Threading.Tasks;
using HTS.Dto.Material;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IMaterialService : IApplicationService
    {
        /// <summary>
        /// Get material by id
        /// </summary>
        /// <param name="id">Desired material id</param>
        /// <returns>Desired material</returns>
        Task<MaterialDto> GetAsync(int id);
        /// <summary>
        /// Get all materials
        /// </summary>
        /// <param name="isActive">IsActive value of material. Default parameter with null value</param>
        /// <returns>material list</returns>
        Task<PagedResultDto<MaterialDto>> GetListAsync(bool? isActive=null);

        /// <summary>
        /// Creates material
        /// </summary>
        /// <param name="material"></param>
        /// <returns>Inserted material</returns>
        Task<MaterialDto> CreateAsync(SaveMaterialDto material);
        
        /// <summary>
        /// Updates material
        /// </summary>
        /// <param name="id">To be updated material id</param>
        /// <param name="material">To be updated information</param>
        /// <returns>Updated material object</returns>
        Task<MaterialDto> UpdateAsync(int id, SaveMaterialDto material);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
