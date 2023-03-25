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

namespace HTS.Interface
{
    public interface IPatientService : IApplicationService
    {
        /// <summary>
        /// Get patient by id
        /// </summary>
        /// <param name="id">Desired patient id</param>
        /// <returns>Desired patient</returns>
        Task<PatientDto> GetAsync(int id);
        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns>Patient list</returns>
        Task<PagedResultDto<PatientDto>> GetListAsync();
        /// <summary>
        /// Creates patient
        /// </summary>
        /// <param name="patient">Patient information to be insert</param>
        /// <returns>Inserted patient</returns>
        Task<PatientDto> CreateAsync(SavePatientDto patient);

        /// <summary>
        /// Updates patient
        /// </summary>patient
        /// <param name="id">To be updated patient id</param>
        /// <param name="patient">To be updated information</param>
        /// <returns>Updated patient object</returns>
        Task<PatientDto> UpdateAsync(int id, SavePatientDto patient);
        
        /// <summary>
        /// Delete given id of patient
        /// </summary>
        /// <param name="id">To be deleted patient id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
