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

        /// <summary>
        /// Approve hospital response
        /// </summary>
        /// <param name="hospitalResponseId">To be approved hospital response id</param>
        /// <returns></returns>
        Task ApproveAsync(int hospitalResponseId);

        /// <summary>
        /// Reject hospital response
        /// </summary>
        /// <param name="hospitalResponseId">To be rejected hospital response id</param>
        /// <returns></returns>
        Task RejectAsync(int hospitalResponseId);

    }
}
