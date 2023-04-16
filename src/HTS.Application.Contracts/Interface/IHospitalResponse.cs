using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.DocumentType;
using HTS.Dto.HospitalResponse;
using HTS.Dto.Major;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

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
        /// Get all hospital responses
        /// </summary>
        /// <returns>Hospital response list</returns>
        Task<PagedResultDto<HospitalResponseDto>> GetListAsync();
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="hospitalResponse">HospitalResponse information to be insert</param>
        /// <returns>Inserted hospital response</returns>
        Task<HospitalResponseDto> CreateAsync(SaveHospitalResponseDto hospitalResponse);
        
        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="id">To be updated entity id</param>
        /// <param name="hospitalResponse">To be updated information</param>
        /// <returns>Updated hospital response object</returns>
        Task<HospitalResponseDto> UpdateAsync(int id, SaveHospitalResponseDto hospitalResponse);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
