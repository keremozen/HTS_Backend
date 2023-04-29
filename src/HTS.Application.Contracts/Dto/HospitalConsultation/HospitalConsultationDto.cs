using HTS.Dto.HospitalConsultationStatus;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.HospitalConsultation;

public class HospitalConsultationDto : AuditedEntityWithUserDto<int, IdentityUserDto>
{
    public string Note { get; set; }
    public int PatientTreatmentProcessId { get; set; }
    public int HospitalId { get; set; }
    public int HospitalConsultationStatusId { get; set; }
    public HospitalConsultationStatusDto HospitalConsultationStatus { get; set; }
    
}