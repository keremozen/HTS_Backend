using System.Threading.Tasks;
using HTS.Dto.InvitationLetterDocument;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IInvitationLetterDocumentService : IApplicationService
    {

        /// <summary>
        /// Creates document
        /// </summary>
        /// <param name="document">Document information to be insert</param>
        /// <returns></returns>
        Task UploadAsync(SaveDocumentDto document);

        /// <summary>
        /// Send email to patient
        /// </summary>
        /// <param name="salesMethodId">Sales Method and Companion Info Entity Id</param>
        /// <returns></returns>
        Task SendEMailToPatient(int salesMethodId);

        /// <summary>
        /// Created invitation letter pdf
        /// </summary>
        /// <param name="salesMethodId"></param>
        /// <returns></returns>
        Task<byte[]> CreateInvitationLetter(int salesMethodId);

    }
}
