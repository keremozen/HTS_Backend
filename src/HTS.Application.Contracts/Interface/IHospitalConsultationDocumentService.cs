using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.Hospital;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalConsultationDocument;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using HTS.Dto.PatientDocument;

namespace HTS.Interface
{
    public interface IHospitalConsultationDocumentService : IApplicationService
    {
        /// <summary>
        /// Get document by Id
        /// </summary>
        /// <param name="id">Document id</param>
        /// <returns>Hospital consultation document</returns>
        Task<HospitalConsultationDocumentDto> GetAsync(int id);

        /// <summary>
        /// Converts patient documents to hospital consultation documents
        /// </summary>
        /// <param name="patientId">Patient info</param>
        /// <returns>Hospital consultation documents</returns>
        public Task<PagedResultDto<HospitalConsultationDocumentDto>> ForwardDocumentsAsync(int patientId);

    }
}
