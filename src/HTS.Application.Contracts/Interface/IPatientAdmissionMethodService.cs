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
using HTS.Dto.PatientAdmissionMethod;

namespace HTS.Interface
{
    public interface IPatientAdmissionMethodService : IApplicationService
    {
        /// <summary>
        /// Get patient  admission method by id
        /// </summary>
        /// <param name="id">Desired patient admission method id</param>
        /// <returns>Desired patient admission method</returns>
        Task<PatientAdmissionMethodDto> GetAsync(int id);
        /// <summary>
        /// Get all patient admission methods
        /// </summary>
        /// <returns>Patient admission method list</returns>
        Task<PagedResultDto<PatientAdmissionMethodDto>> GetListAsync();
        /// <summary>
        /// Creates patient admission method
        /// </summary>
        /// <param name="patientAdmissionMethod">Patient admission method information to be insert</param>
        /// <returns>Inserted patient admission method</returns>
        Task<PatientAdmissionMethodDto> CreateAsync(SavePatientAdmissionMethodDto patientAdmissionMethod);

        /// <summary>
        /// Updates patient admission method
        /// </summary>
        /// <param name="id">To be updated patient admission method id</param>
        /// <param name="patient">To be updated information</param>
        /// <returns>Updated patient admission method object</returns>
        Task<PatientAdmissionMethodDto> UpdateAsync(int id, SavePatientAdmissionMethodDto patientAdmissionMethod);

        /// <summary>
        /// Delete given id of patient admission method
        /// </summary>
        /// <param name="id">To be deleted patient admission method id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
