using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.Hospital;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface IHospitalConsultationService : IApplicationService
    {
        /// <summary>
        /// Get entity list by patient treatment process
        /// </summary>
        /// <param name="ptpId">Patient treatment process id</param>
        /// <returns>HospitalConsultation list by ptp</returns>
        public Task<PagedResultDto<HospitalConsultationDto>> GetByPatientTreatmenProcessAsync(int ptpId);
        
        /// <summary>
        /// Saves hospital consultation data with documents
        /// </summary>
        /// <param name="hospitalConsultation">Data to be saved</param>
        /// <returns></returns>
        public Task CreateAsync(SaveHospitalConsultationDto hospitalConsultation);
        
        /// <summary>
        /// Delete given id of hospital
        /// </summary>
        /// <param name="id">To be deleted hospital id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
