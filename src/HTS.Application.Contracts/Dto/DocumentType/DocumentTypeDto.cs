using Volo.Abp.Application.Dtos;

namespace HTS.Dto.DocumentType;

public class DocumentTypeDto : EntityDto<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}