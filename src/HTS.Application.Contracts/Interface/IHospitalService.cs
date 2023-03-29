using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.Hospital;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface IHospitalService : IApplicationService
    {
        /// <summary>
        /// Get nationality by id
        /// </summary>
        /// <param name="id">Desired hospital id</param>
        /// <returns>Desired nationality</returns>
        Task<HospitalDto> GetAsync(int id);
        /// <summary>
        /// Get all nationalities
        /// </summary>
        /// <returns>Nationality list</returns>
        Task<PagedResultDto<HospitalDto>> GetListAsync();
        /// <summary>
        /// Creates hospital
        /// </summary>
        /// <param name="hospital">Hospital information to be insert</param>
        /// <returns>Inserted hospital</returns>
        Task<HospitalDto> CreateAsync(SaveHospitalDto hospital);
        
        /// <summary>
        /// Updates hospital
        /// </summary>
        /// <param name="id">To be updated hospital id</param>
        /// <param name="hospital">To be updated information</param>
        /// <returns>Updated hospital object</returns>
        Task<HospitalDto> UpdateAsync(int id, SaveHospitalDto hospital);
        
        /// <summary>
        /// Delete given id of hospital
        /// </summary>
        /// <param name="id">To be deleted hospital id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
