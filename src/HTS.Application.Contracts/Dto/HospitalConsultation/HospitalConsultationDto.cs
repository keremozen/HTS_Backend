using HTS.Dto.HospitalConsultationDocument;
using HTS.Dto.HospitalConsultationStatus;
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
    public HospitalConsultationStatusEnum HospitalConsultationStatusId { get; set; }
    public HospitalConsultationStatusDto HospitalConsultationStatus { get; set; }
}