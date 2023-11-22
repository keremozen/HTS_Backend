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
using HTS.Dto.HospitalAgentNote;

namespace HTS.Interface
{
    public interface IHospitalAgentNoteService : IApplicationService
    {
      
        /// <summary>
        /// Get all data by hospital response id
        /// </summary>
        /// <param name="hospitalResponseId">Hospital response id</param>
        /// <returns>Hospital agent note list</returns>
       Task<PagedResultDto<HospitalAgentNoteDto>> GetListAsync(int hospitalResponseId);

        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="agentNote">Data to be insert</param>
        /// <returns>Inserted object</returns>
        Task<HospitalAgentNoteDto> CreateAsync(SaveHospitalAgentNoteDto agentNote);

        /// <summary>
        /// Updates entity status
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="statusId">To be updated status</param>
        /// <returns>Updated object</returns>
        Task<HospitalAgentNoteDto> UpdateStatus(int id, int statusId);

        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
