using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

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
        /// <returns>Language list</returns>
        Task<PagedResultDto<LanguageDto>> GetListAsync();
        Task<LanguageDto> CreateAsync(SaveLanguageDto language);
        
        Task UpdateAsync(int id, SaveLanguageDto language);
        
        /// <summary>
        /// Delete given id of language
        /// </summary>
        /// <param name="id">To be deleted language id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
