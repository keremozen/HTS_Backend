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

namespace HTS.Interface
{
    public interface IContractedInstitutionStaffService : IApplicationService
    {
        /// <summary>
        /// Get all contracted institution staffs by institution
        /// </summary>
        /// <param name="institutionId">Institution to get staffs</param>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Contracted institution staff list</returns>
        Task<PagedResultDto<ContractedInstitutionStaffDto>> GetByInstitutionListAsync(int institutionId, bool? isActive=null);
        
        /// <summary>
        /// Get contracted institution staff by id
        /// </summary>
        /// <param name="id">Desired object id</param>
        /// <returns>Desired object</returns>
        Task<ContractedInstitutionStaffDto> GetAsync(int id);

        /// <summary>
        /// Creates contracted institution staff
        /// </summary>
        /// <param name="contractedInstitutionStaff">contracted institution staff information to insert</param>
        /// <returns></returns>
        Task CreateAsync(SaveContractedInstitutionStaffDto contractedInstitutionStaff);
        
        /// <summary>
        /// Creates contracted institution staff of institution
        /// </summary>
        /// <param name="id">To be updated contracted institution staff id</param>
        /// <param name="contractedInstitutionStaff">contracted institution staff information to be updated</param>
        /// <returns></returns>
        Task UpdateAsync(int id, SaveContractedInstitutionStaffDto contractedInstitutionStaff);
        

        /// <summary>
        /// Delete given id of contracted institution staff
        /// </summary>
        /// <param name="id">To be deleted contracted institution staff id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
        
       
        
    }
}
