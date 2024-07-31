using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using HTS.Dto.HospitalInterpreter;

namespace HTS.Interface
{
    public interface IHospitalInterpreterService : IApplicationService
    {
        /// <summary>
        /// Get all hospital interpreters by hospital
        /// </summary>
        /// <param name="hospitalId">Hospital id to get interpreters</param>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Hospital interpreter list</returns>
        Task<PagedResultDto<HospitalInterpreterDto>> GetByHospitalListAsync(int hospitalId, bool? isActive = null);
        
        /// <summary>
        /// Creates hospital interpreter
        /// </summary>
        /// <param name="hospitalInterpreter">Hospital interpreter information to be inserted</param>
        /// <returns>Action response of insert</returns>
        Task CreateAsync(SaveHospitalInterpreterDto hospitalInterpreter);
        
        /// <summary>
        /// Updates hospital interpreter
        /// </summary>
        /// <param name="id">To be updated hospital interpreter id</param>
        /// <param name="hospitalInterpreter">To be updated information</param>
        /// <returns>Action response of update</returns>
        Task UpdateAsync(int id, SaveHospitalInterpreterDto hospitalInterpreter);
        
        /// <summary>
        /// Delete given id of hospital interpreter
        /// </summary>
        /// <param name="id">To be deleted hospital interpreter id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}