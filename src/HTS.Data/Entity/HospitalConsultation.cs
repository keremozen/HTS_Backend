using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalConsultation : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public string Note { get; set; }
        [Required]
        public bool IsActive { get; set; }    
        public Hospital Hospital { get; set; }
        public HospitalConsultationStatus HospitalConsultationStatus { get; set; }
        public PatientTreatmentProcess PatientTreatmentProcess { get; set; }

    }
}
