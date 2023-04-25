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
using HTS.Dto.Hospital;
using HTS.Dto.HospitalStaff;
using Microsoft.AspNetCore.Mvc;

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
        /// Creates hospital staff
        /// </summary>
        /// <param name="hospitalStaff">Hospital staff information to be insert</param>
        /// <returns>Action response of insert</returns>
        Task<IActionResult> CreateAsync(SaveHospitalStaffDto hospitalStaff);
        
        /// <summary>
        /// Updates hospital staff
        /// </summary>
        /// <param name="id">To be updated hospital id</param>
        /// <param name="hospitalStaff">To be updated information</param>
        /// <returns>Action response of update</returns>
        Task<IActionResult> UpdateAsync(int id, SaveHospitalStaffDto hospitalStaff);
        

        /// <summary>
        /// Delete given id of hospital staff
        /// </summary>
        /// <param name="id">To be deleted hospital staff id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
