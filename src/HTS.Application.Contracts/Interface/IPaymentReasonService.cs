using System.Threading.Tasks;
using HTS.Dto.Nationality;
using HTS.Dto.PaymentReason;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IPaymentReasonService : IApplicationService
    {
        /// <summary>
        /// Get payment reason by id
        /// </summary>
        /// <param name="id">Desired payment reason id</param>
        /// <returns>Result object</returns>
        Task<PaymentReasonDto> GetAsync(int id);
        /// <summary>
        /// Get all objects
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Payment reason list</returns>
        Task<ListResultDto<PaymentReasonDto>> GetListAsync(bool? isActive=null);

        /// <summary>
        /// Creates payment reason
        /// </summary>
        /// <param name="paymentReason">To be inserted object</param>
        /// <returns></returns>
        Task CreateAsync(SavePaymentReasonDto paymentReason);

        /// <summary>
        /// Updates nationality
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="paymentReason">To be updated object</param>
        /// <returns></returns>
        Task UpdateAsync(int id, SavePaymentReasonDto paymentReason);
        
        /// <summary>
        /// Delete given id of payment reason
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
