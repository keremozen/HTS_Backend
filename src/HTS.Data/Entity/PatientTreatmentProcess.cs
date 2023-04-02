using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Data.Entity
{

    public class PatientTreatmentProcess : FullAuditedEntity<int>
    {
        [Required]
        public string TreatmentCode { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int TreatmentProcessStatusId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("TreatmentProcessStatusId")]
        public TreatmentProcessStatus TreatmentProcessStatus { get; set; }
        public virtual ICollection<HospitalConsultation> HospitalConsultations { get; set; }
    }
}
