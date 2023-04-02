using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.PatientTreatmentProcess;

public class PatientTreatmentProcessDto  : AuditedEntityWithUserDto<int,IdentityUserDto>
{
    public string TreatmentCode { get; set; }
    public EntityEnum.PatientTreatmentStatusEnum TreatmentProcessStatusId { get; set; }
}