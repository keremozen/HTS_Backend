using HTS.Dto;
using HTS.Dto.City;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface ICityService : IApplicationService
    {
        /// <summary>
        /// Get city by id
        /// </summary>
        /// <param name="id">Desired city id</param>
        /// <returns>Desired City</returns>
        Task<CityDto> GetAsync(int id);
        /// <summary>
        /// Get all citys
        /// </summary>
        /// <returns>City list</returns>
        Task<PagedResultDto<CityDto>> GetListAsync();
        /// <summary>
        /// Creates city
        /// </summary>
        /// <param name="city">City information to be insert</param>
        /// <returns>Inserted City</returns>
        Task<CityDto> CreateAsync(SaveCityDto city);
        
        /// <summary>
        /// Updates languge
        /// </summary>
        /// <param name="id">To be updated city id</param>
        /// <param name="city">To be updated information</param>
        /// <returns>Updated city object</returns>
        Task<CityDto> UpdateAsync(int id, SaveCityDto city);
        
        /// <summary>
        /// Delete given id of city
        /// </summary>
        /// <param name="id">To be deleted city id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
