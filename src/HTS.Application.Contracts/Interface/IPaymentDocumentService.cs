using System.Threading.Tasks;
using HTS.Dto.PatientDocument;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using HTS.Dto.PatientNote;
using HTS.Dto.PaymentDocument;

namespace HTS.Interface
{
    public interface IPaymentDocumentService : IApplicationService
    {

        /// <summary>
        /// Get document by Id
        /// </summary>
        /// <param name="id">Document id</param>
        /// <returns>Payment document</returns>
        Task<PaymentDocumentDto> GetAsync(int id);

        /// <summary>
        /// Save payment document
        /// </summary>
        /// <param name="paymentDocument">Payment document to be insert</param>
        /// <returns></returns>
        public Task SaveAsync(SavePaymentDocumentDto paymentDocument);
            
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
