using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.DocumentType;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class DocumentTypeService : ApplicationService, IDocumentTypeService
{
    private readonly IRepository<DocumentType, int> _documentTypeRepository;
    public DocumentTypeService(IRepository<DocumentType, int> documentTypeRepository) 
    {
        _documentTypeRepository = documentTypeRepository;
    }
    
    public async Task<DocumentTypeDto> GetAsync(int id)
    {
        return ObjectMapper.Map<DocumentType, DocumentTypeDto>(await _documentTypeRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<DocumentTypeDto>> GetListAsync(bool? isActive=null)
    {
        //Get all entities
        var query = await _documentTypeRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            d => d.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<DocumentType>, List<DocumentTypeDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _documentTypeRepository.CountAsync();//item count
        return new PagedResultDto<DocumentTypeDto>(totalCount,responseList);
    }

    public async Task<DocumentTypeDto> CreateAsync(SaveDocumentTypeDto documentType)
    {
        var entity = ObjectMapper.Map<SaveDocumentTypeDto, DocumentType>(documentType);
        await _documentTypeRepository.InsertAsync(entity);
        return ObjectMapper.Map<DocumentType, DocumentTypeDto>(entity);
    }

    public async Task<DocumentTypeDto> UpdateAsync(int id, SaveDocumentTypeDto documentType)
    {
        var entity = await _documentTypeRepository.GetAsync(id);
        ObjectMapper.Map(documentType, entity);
        return ObjectMapper.Map<DocumentType,DocumentTypeDto>( await _documentTypeRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _documentTypeRepository.DeleteAsync(id);
    }
}