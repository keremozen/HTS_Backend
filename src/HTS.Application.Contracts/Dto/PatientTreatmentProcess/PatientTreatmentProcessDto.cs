using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.PatientTreatmentProcess;

public class PatientTreatmentProcessDto  : AuditedEntityWithUserDto<int,IdentityUserDto>
{
    public string TreatmentCode { get; set; }
    public PatientTreatmentStatusEnum TreatmentProcessStatusId { get; set; }
}