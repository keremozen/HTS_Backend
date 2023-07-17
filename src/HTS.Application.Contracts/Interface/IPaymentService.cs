using System.Threading.Tasks;
using HTS.Dto.Payment;
using HTS.Dto.Process;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IPaymentService : IApplicationService
    {

        /// <summary>
        /// Get payment by id
        /// </summary>
        /// <param name="id">Payment Id</param>
        /// <returns>Payment</returns>
        Task<PaymentDto> GetAsync(int id);
        
        /// <summary>
        /// Get all payments
        /// </summary>
        /// <param name="ptpId">Patient treatment process id</param>
        /// <returns>Object list</returns>
        Task<PagedResultDto<ListPaymentDto>> GetListAsync(int ptpId);

        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="payment">Payment data</param>
        /// <returns>Inserted entity object</returns>
        Task CreateAsync(SavePaymentDto payment);


        Task CreateInvoicePdf(int id);


    }
}
