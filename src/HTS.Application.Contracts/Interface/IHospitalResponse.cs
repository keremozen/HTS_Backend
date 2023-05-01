using System.Threading.Tasks;
using HTS.Dto.HospitalResponse;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IHospitalResponseService : IApplicationService
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Desired entity id</param>
        /// <returns>Desired entity</returns>
        Task<HospitalResponseDto> GetAsync(int id);
   
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="hospitalResponse">HospitalResponse information to be insert</param>
        /// <returns></returns>
        Task CreateAsync(SaveHospitalResponseDto hospitalResponse);
        
    }
}
