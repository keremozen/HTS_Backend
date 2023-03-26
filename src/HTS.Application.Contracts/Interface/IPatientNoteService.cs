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
using HTS.Dto.PatientNote;

namespace HTS.Interface
{
    public interface IPatientNoteService : IApplicationService
    {
      
        /// <summary>
        /// Get all patientnotes by patient id
        /// </summary>
        /// <param name="patientId">Patient id</param>
        /// <returns>Patient note list</returns>
        Task<PagedResultDto<PatientNoteDto>> GetListAsync(int patientId);
        /// <summary>
        /// Creates patient note
        /// </summary>
        /// <param name="patientNote">Patient note information to be insert</param>
        /// <returns>Inserted patient note</returns>
        Task<PatientNoteDto> CreateAsync(SavePatientNoteDto patientNote);

        /// <summary>
        /// Updates patient note status
        /// </summary>
        /// <param name="id">To be updated patient note id</param>
        /// <param name="statusId">To be updated status</param>
        /// <returns>Updated patient note object</returns>
        Task<PatientNoteDto> UpdateStatus(int id, int statusId);

        /// <summary>
        /// Delete given id of patient note
        /// </summary>
        /// <param name="id">To be deleted patient note id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
