﻿using HTS.Dto.Language;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface ILanguageService : IApplicationService
    {
        /// <summary>
        /// Get language by id
        /// </summary>
        /// <param name="id">Desired language id</param>
        /// <returns>Desired Language</returns>
        Task<LanguageDto> GetAsync(int id);
        /// <summary>
        /// Get all languages
        /// </summary>
        /// <param name="isActive">IsActive value of data. Default parameter with null value</param>
        /// <returns>Language list</returns>
        Task<PagedResultDto<LanguageDto>> GetListAsync(bool? isActive=null);
        /// <summary>
        /// Creates language
        /// </summary>
        /// <param name="language">Language information to be insert</param>
        /// <returns>Inserted Language</returns>
        Task<LanguageDto> CreateAsync(SaveLanguageDto language);

        /// <summary>
        /// Creates languages
        /// </summary>
        /// <param name="languages">Languages information to be insert</param>
        /// <returns></returns>
        Task CreateListAsync(List<SaveLanguageDto> languages);
        
        /// <summary>
        /// Updates languge
        /// </summary>
        /// <param name="id">To be updated language id</param>
        /// <param name="language">To be updated information</param>
        /// <returns>Updated language object</returns>
        Task<LanguageDto> UpdateAsync(int id, SaveLanguageDto language);


        
        /// <summary>
        /// Delete given id of language
        /// </summary>
        /// <param name="id">To be deleted language id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
