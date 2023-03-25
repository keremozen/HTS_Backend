using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
namespace HTS.Data.Entity
{

    public class HospitalConsultation : FullAuditedEntity<int>
    {
        [Required]
        public string Note { get; set; }
        public bool IsActive { get; set; }    
        public Hospital Hospital { get; set; }
        public HospitalConsultationStatus HospitalConsultationStatus { get; set; }
        public PatientTreatmentProcess PatientTreatmentProcess { get; set; }

    }
}
