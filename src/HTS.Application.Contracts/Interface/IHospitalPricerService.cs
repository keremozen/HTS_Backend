using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using HTS.Dto.HospitalPricer;

namespace HTS.Interface
{
    public interface IHospitalPricerService : IApplicationService
    {
        /// <summary>
        /// Get all hospital pricers by hospital
        /// </summary>
        /// <param name="hospitalId">Hospital id to get pricers</param>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Hospital pricer list</returns>
        Task<PagedResultDto<HospitalPricerDto>> GetByHospitalListAsync(int hospitalId, bool? isActive = null);
        
        /// <summary>
        /// Creates hospital pricer
        /// </summary>
        /// <param name="hospitalPricer">Hospital pricer information to be insert</param>
        /// <returns>Action response of insert</returns>
        Task CreateAsync(SaveHospitalPricerDto hospitalPricer);
        
        /// <summary>
        /// Updates hospital pricer
        /// </summary>
        /// <param name="id">To be updated hospital id</param>
        /// <param name="hospitalPricer">To be updated information</param>
        /// <returns>Action response of update</returns>
        Task UpdateAsync(int id, SaveHospitalPricerDto hospitalPricer);
        

        /// <summary>
        /// Delete given id of hospital pricer
        /// </summary>
        /// <param name="id">To be deleted hospital pricer id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
