using System.Buffers.Text;
using HTS.Dto.DocumentType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.PatientDocument;

public class PatientDocumentDto: AuditedEntityWithUserDto<int, IdentityUserDto>
{
    public string Description { get; set; }
    public string FileName { get; set; }
    public int PatientId { get; set; }
    public string File { get; set; }
    public string ContentType { get; set; }
    public DocumentTypeDto DocumentType { get; set; }
    public PatientDocumentStatusEnum PatientDocumentStatusId { get; set; }
}