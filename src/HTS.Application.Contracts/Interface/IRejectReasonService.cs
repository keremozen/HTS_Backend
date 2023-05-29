using System.Threading.Tasks;
using HTS.Dto.RejectReason;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IRejectReasonService : IApplicationService
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired entity</returns>
        Task<RejectReasonDto> GetAsync(int id);
        /// <summary>
        /// Get all reject reasons
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Reject reason list</returns>
        Task<PagedResultDto<RejectReasonDto>> GetListAsync(bool? isActive=null);
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="rejectReason">Reject reason information to be insert</param>
        /// <returns>Inserted entity object</returns>
        Task<RejectReasonDto> CreateAsync(SaveRejectReasonDto rejectReason);
        
        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="rejectReason">To be updated information</param>
        /// <returns>Updated object</returns>
        Task<RejectReasonDto> UpdateAsync(int id, SaveRejectReasonDto rejectReason);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
