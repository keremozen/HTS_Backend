using HTS.Dto.Patient;
using HTS.Dto.TreatmentProcessStatus;
using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.PatientTreatmentProcess;

public class PatientTreatmentProcessDetailedDto  : AuditedEntityWithUserDto<int,IdentityUserDto>
{
    public string TreatmentCode { get; set; }
    public decimal HBYSPrice { get; set; }
    public decimal ProformaPrice { get; set; }
    public decimal PaymentPrice { get; set; }
    public decimal UnPaidPrice { get; set; }
    public bool IsFinalized { get; set; }
    public int? FinalizationTypeId { get; set; }
    public string? FinalizationDescription { get; set; }
    public PatientTreatmentStatusEnum TreatmentProcessStatusId { get; set; }
    public TreatmentProcessStatusDto TreatmentProcessStatus { get; set; }
}