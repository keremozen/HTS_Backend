using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using HTS.Dto.Patient;
using HTS.Dto.PatientTreatmentProcess;

namespace HTS.Interface
{
    public interface IPatientTreatmentProcessService : IApplicationService
    {
        /// <summary>
        /// Patient's treatment process list
        /// </summary>
        /// <param name="patientId">Patient</param>
        /// <returns>Patient's treatment process list</returns>
        public Task<PagedResultDto<PatientTreatmentProcessDetailedDto>> GetListByPatientIdAsync(int patientId);
        /// <summary>
        /// Starts patient treatment process
        /// </summary>
        /// <param name="patientId">Patient id to start treatment process</param>
        /// <returns>Inserted patient treatment process</returns>
        Task<PatientTreatmentProcessDto> StartAsync(int patientId);
        
        /// <summary>
        /// Finalizes ptp
        /// </summary>
        /// <param name="id">To be finalized ptp Id</param>
        /// <param name="finalizePtp">Finalize information</param>
        /// <returns></returns>
        Task FinalizeAsync(int id, FinalizePtpDto finalizePtp);
        
        /// <summary>
        /// De finalizes the ptp
        /// </summary>
        /// <param name="ptpId">To be de finalized ptp id</param>
        /// <returns></returns>
        Task DeFinalizeAsync(int ptpId);

    }
}
