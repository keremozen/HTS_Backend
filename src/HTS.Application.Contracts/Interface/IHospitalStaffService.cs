using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
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
        /// Creates hospital staff
        /// </summary>
        /// <param name="hospitalStaff">Hospital staff information to be insert</param>
        /// <returns>Action response of insert</returns>
        Task CreateAsync(SaveHospitalStaffDto hospitalStaff);
        
        /// <summary>
        /// Updates hospital staff
        /// </summary>
        /// <param name="id">To be updated hospital id</param>
        /// <param name="hospitalStaff">To be updated information</param>
        /// <returns>Action response of update</returns>
        Task UpdateAsync(int id, SaveHospitalStaffDto hospitalStaff);
        

        /// <summary>
        /// Delete given id of hospital staff
        /// </summary>
        /// <param name="id">To be deleted hospital staff id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
