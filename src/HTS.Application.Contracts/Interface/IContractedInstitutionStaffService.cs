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
        /// <returns>Contracted institution staff list</returns>
        Task<PagedResultDto<ContractedInstitutionStaffDto>> GetByInstitutionListAsync(int institutionId);
        
        /// <summary>
        /// Creates contracted institution staffs of institution
        /// </summary>
        /// <param name="institutionId">To be saved staffs of contracted institution id</param>
        /// <param name="contractedInstitutionStaffs">contracted institution information to be insert</param>
        /// <returns></returns>
        Task SaveAsync(int institutionId, List<SaveContractedInstitutionStaffDto> contractedInstitutionStaffs);

        /// <summary>
        /// Delete given id of contracted institution staff
        /// </summary>
        /// <param name="id">To be deleted contracted institution staff id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
