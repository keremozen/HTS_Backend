using System.Threading.Tasks;
using HTS.Dto.PatientDocument;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using HTS.Dto.PatientNote;

namespace HTS.Interface
{
    public interface IPatientDocumentService : IApplicationService
    {

        /// <summary>
        /// Get document by Id
        /// </summary>
        /// <param name="id">Document id</param>
        /// <returns>Patient document</returns>
        Task<PatientDocumentDto> GetAsync(int id);
      
        /// <summary>
        /// Get all data by patient id
        /// </summary>
        /// <param name="patientId">Patient id</param>
        /// <returns>Patient document list</returns>
        Task<PagedResultDto<PatientDocumentDto>> GetListAsync(int patientId);
        /// <summary>
        /// Creates patient document
        /// </summary>
        /// <param name="patientDocument">Patient document information to be insert</param>
        /// <returns>Inserted object</returns>
        Task<PatientDocumentDto> CreateAsync(SavePatientDocumentDto patientDocument);

        /// <summary>
        /// Updates patient document status
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="statusId">To be updated status</param>
        /// <returns>Updated object</returns>
        Task<PatientDocumentDto> UpdateStatus(int id, int statusId);

        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
