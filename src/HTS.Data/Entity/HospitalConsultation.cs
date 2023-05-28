using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalConsultation : FullAuditedEntityWithUser<int, IdentityUser>
    {
        public HospitalConsultation()
        {
            HospitalResponses = new HashSet<HospitalResponse>();
        }

        [Required]
        public string Note { get; set; }
        [Required]
        public int PatientTreatmentProcessId { get; set; }
        [Required]
        public int HospitalId { get; set; }
        [Required]
        public int HospitalConsultationStatusId { get; set; }
        [ForeignKey("HospitalId")]
        public Hospital Hospital { get; set; }
        [ForeignKey("HospitalConsultationStatusId")]
        public HospitalConsultationStatus HospitalConsultationStatus { get; set; }
        [ForeignKey("PatientTreatmentProcessId")]
        public PatientTreatmentProcess PatientTreatmentProcess { get; set; }
        public virtual ICollection<HospitalConsultationDocument> HospitalConsultationDocuments { get; set; }
        public virtual ICollection<HospitalResponse> HospitalResponses { get; set; }

    }
}
