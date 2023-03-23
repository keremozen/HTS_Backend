using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.DocumentType;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface IDocumentTypeService : IApplicationService
    {
        /// <summary>
        /// Get document type by id
        /// </summary>
        /// <param name="id">Desired document type id</param>
        /// <returns>Desired document type</returns>
        Task<DocumentTypeDto> GetAsync(int id);
        /// <summary>
        /// Get all document types
        /// </summary>
        /// <returns>document type list</returns>
        Task<PagedResultDto<DocumentTypeDto>> GetListAsync();
        /// <summary>
        /// Creates document type
        /// </summary>
        /// <param name="documentType">Document type information to be insert</param>
        /// <returns>Inserted document type</returns>
        Task<DocumentTypeDto> CreateAsync(SaveDocumentTypeDto documentType);
        
        /// <summary>
        /// Updates document type
        /// </summary>
        /// <param name="id">To be updated document type id</param>
        /// <param name="documentType">To be updated information</param>
        /// <returns>Updated document type object</returns>
        Task<DocumentTypeDto> UpdateAsync(int id, SaveDocumentTypeDto documentType);
        
        /// <summary>
        /// Delete given id of document type
        /// </summary>
        /// <param name="id">To be deleted document type id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
