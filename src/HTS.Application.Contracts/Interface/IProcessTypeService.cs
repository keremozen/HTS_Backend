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
    }
}
