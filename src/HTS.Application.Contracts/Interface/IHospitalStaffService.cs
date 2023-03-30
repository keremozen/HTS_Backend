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
using HTS.Dto.ContractedInstitution;
using HTS.Dto.ContractedInstitutionStaff;
using HTS.Dto.HospitalStaff;

namespace HTS.Interface
{
    public interface IHospitalStaffService : IApplicationService
    {
        /// <summary>
        /// Get all hospital staffs by institution
        /// </summary>
        /// <param name="hospitalId">Hospital id to get staffs</param>
        /// <returns>Hospital staff list</returns>
        Task<PagedResultDto<HospitalStaffDto>> GetByInstitutionListAsync(int hospitalId);
        
        /// <summary>
        /// Creates hospital staffs of institution
        /// </summary>
        /// <param name="hospitalId">To be saved staffs of hospital id</param>
        /// <param name="hospitalStaffs">hospital information to be insert</param>
        /// <returns></returns>
        Task SaveAsync(int hospitalId, List<SaveHospitalStaffDto> hospitalStaffs);

        /// <summary>
        /// Delete given id of hospital staff
        /// </summary>
        /// <param name="id">To be deleted hospital staff id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
