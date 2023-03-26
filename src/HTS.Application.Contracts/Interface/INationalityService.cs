﻿using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface INationalityService : IApplicationService
    {
        /// <summary>
        /// Get nationality by id
        /// </summary>
        /// <param name="id">Desired nationality id</param>
        /// <returns>Desired nationality</returns>
        Task<NationalityDto> GetAsync(int id);
        /// <summary>
        /// Get all nationalities
        /// </summary>
        /// <returns>Nationality list</returns>
        Task<PagedResultDto<NationalityDto>> GetListAsync();
        /// <summary>
        /// Creates nationality
        /// </summary>
        /// <param name="nationality">Nationality information to be insert</param>
        /// <returns>Inserted nationality</returns>
        Task<NationalityDto> CreateAsync(SaveNationalityDto nationality);
        
        /// <summary>
        /// Updates nationality
        /// </summary>
        /// <param name="id">To be updated nationality id</param>
        /// <param name="nationality">To be updated information</param>
        /// <returns>Updated nationality object</returns>
        Task<NationalityDto> UpdateAsync(int id, SaveNationalityDto nationality);
        
        /// <summary>
        /// Delete given id of nationality
        /// </summary>
        /// <param name="id">To be deleted nationality id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}