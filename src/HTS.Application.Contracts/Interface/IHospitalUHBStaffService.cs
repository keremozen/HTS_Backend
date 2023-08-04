using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using HTS.Dto.HospitalStaff;
using HTS.Dto.HospitalUHBStaff;

namespace HTS.Interface
{
    public interface IHospitalUHBStaffService : IApplicationService
    {
        /// <summary>
        /// Get all hospital staffs by hospital
        /// </summary>
        /// <param name="hospitalId">Hospital id to get staffs</param>
        /// <returns>Hospital staff list</returns>
        Task<PagedResultDto<HospitalUHBStaffDto>> GetByHospitalListAsync(int hospitalId);
        
        /// <summary>
        /// Creates hospital uhb staff
        /// </summary>
        /// <param name="hospitalStaff">Hospital staff information to be insert</param>
        /// <returns>Action response of insert</returns>
        Task CreateAsync(SaveHospitalUHBStaffDto hospitalStaff);
        
        /// <summary>
        /// Updates hospital uhb staff
        /// </summary>
        /// <param name="id">To be updated hospital id</param>
        /// <param name="hospitalStaff">To be updated information</param>
        /// <returns>Action response of update</returns>
        Task UpdateAsync(int id, SaveHospitalUHBStaffDto hospitalStaff);
        

        /// <summary>
        /// Delete given id of hospital uhb staff
        /// </summary>
        /// <param name="id">To be deleted hospital uhb staff id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
