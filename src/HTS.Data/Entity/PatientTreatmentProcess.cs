using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class PatientTreatmentProcess : AuditedEntityWithUser<int,IdentityUser>
    {
        [Required]
        public string TreatmentCode { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int TreatmentProcessStatusId { get; set; }

        [Required]
        public bool IsFinalized { get; set; } = false;
        public int? FinalizationTypeId { get; set; }
        public string? FinalizationDescription { get; set; }
        public bool? Completed { get; set; }
    
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("TreatmentProcessStatusId")]
        public TreatmentProcessStatus TreatmentProcessStatus { get; set; }
        [ForeignKey("FinalizationTypeId")]
        public FinalizationType? FinalizationType { get; set; }
        public virtual ICollection<HospitalConsultation> HospitalConsultations { get; set; }
    }
}
