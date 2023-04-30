using HTS.Dto.DocumentType;
using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.HospitalConsultationDocument;

public class HospitalConsultationDocumentDto: AuditedEntityWithUserDto<int, IdentityUserDto>
{
    public int HospitalConsultationId { get; set; }
    public int DocumentTypeId { get; set; }
    public int PatientDocumentStatusId { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    public byte[] File { get; set; }
    public DocumentTypeDto DocumentType { get; set; }
    public EntityEnum.PatientDocumentStatusEnum PatientDocumentStatus { get; set; }
    
}