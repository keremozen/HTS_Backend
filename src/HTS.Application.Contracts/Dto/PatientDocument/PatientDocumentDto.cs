using HTS.Dto.DocumentType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.PatientDocument;

public class PatientDocumentDto: AuditedEntityWithUserDto<int, IdentityUserDto>
{
    public string Description { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public int PatientId { get; set; }
    public DocumentTypeDto DocumentType { get; set; }
    public PatientDocumentStatusEnum PatientDocumentStatus { get; set; }

}