using HTS.Dto.Hospital;
using HTS.Dto.HospitalConsultationDocument;
using HTS.Dto.HospitalConsultationStatus;
using HTS.Dto.PatientTreatmentProcess;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.HospitalConsultation;

public class HospitalConsultationDto : AuditedEntityWithUserDto<int, IdentityUserDto>
{
    public string Note { get; set; }
    public int PatientTreatmentProcessId { get; set; }
    public int HospitalId { get; set; }
    public int RowNumber { get; set; }
    public HospitalConsultationStatusEnum HospitalConsultationStatusId { get; set; }
    public HospitalConsultationStatusDto HospitalConsultationStatus { get; set; }
    public List<HospitalConsultationDocumentDto> HospitalConsultationDocuments { get; set; }
    public HospitalDto Hospital { get; set; }
    public PatientTreatmentProcessDto PatientTreatmentProcess { get; set; }
}