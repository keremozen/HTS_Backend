using HTS.Dto.Branch;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IBranchService : IApplicationService
    {
        /// <summary>
        /// Get major by id
        /// </summary>
        /// <param name="id">Desired major id</param>
        /// <returns>Desired major</returns>
        Task<BranchDto> GetAsync(int id);
        /// <summary>
        /// Get all majors
        /// </summary>
        /// <returns>Major list</returns>
        Task<PagedResultDto<BranchDto>> GetListAsync();
        /// <summary>
        /// Creates major
        /// </summary>
        /// <param name="major">Major information to be insert</param>
        /// <returns>Inserted major</returns>
        Task<BranchDto> CreateAsync(SaveBranchDto major);
        
        /// <summary>
        /// Updates major
        /// </summary>
        /// <param name="id">To be updated major id</param>
        /// <param name="major">To be updated information</param>
        /// <returns>Updated major object</returns>
        Task<BranchDto> UpdateAsync(int id, SaveBranchDto major);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
