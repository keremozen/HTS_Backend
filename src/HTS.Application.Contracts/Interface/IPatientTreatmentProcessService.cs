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
        /// Starts patient treatment process
        /// </summary>
        /// <param name="patientId">Patient id to start treatment process</param>
        /// <returns>Inserted patient treatment process</returns>
        Task<PatientTreatmentProcessDto> StartAsync(int patientId);
        
    }
}
