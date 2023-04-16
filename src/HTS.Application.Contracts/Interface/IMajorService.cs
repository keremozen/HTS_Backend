using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.DocumentType;
using HTS.Dto.Major;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface IMajorService : IApplicationService
    {
        /// <summary>
        /// Get major by id
        /// </summary>
        /// <param name="id">Desired major id</param>
        /// <returns>Desired major</returns>
        Task<MajorDto> GetAsync(int id);
        /// <summary>
        /// Get all majors
        /// </summary>
        /// <returns>Major list</returns>
        Task<PagedResultDto<MajorDto>> GetListAsync();
        /// <summary>
        /// Creates major
        /// </summary>
        /// <param name="major">Major information to be insert</param>
        /// <returns>Inserted major</returns>
        Task<MajorDto> CreateAsync(SaveMajorDto major);
        
        /// <summary>
        /// Updates major
        /// </summary>
        /// <param name="id">To be updated major id</param>
        /// <param name="major">To be updated information</param>
        /// <returns>Updated major object</returns>
        Task<MajorDto> UpdateAsync(int id, SaveMajorDto major);
        
        /// <summary>
        /// Delete given id of entity
        /// </summary>
        /// <param name="id">To be deleted entity id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
