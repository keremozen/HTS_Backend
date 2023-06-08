using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalResponse : FullAuditedEntityWithUser<int, IdentityUser>
    {
        public int? HospitalConsultationId { get; set; }
        public string? Description { get; set; }
        [Required]
        public int HospitalResponseTypeId { get; set; }

        public int? HospitalizationTypeId { get; set; }

        public DateTime? PossibleTreatmentDate { get; set; }
        public int? HospitalizationNumber { get; set; }

        [ForeignKey("HospitalResponseTypeId")]
        public HospitalResponseType HospitalResponseType { get; set; }

        [ForeignKey("HospitalConsultationId")]
        public HospitalConsultation? HospitalConsultation { get; set; }

        [ForeignKey("HospitalizationTypeId")]
        public HospitalizationType HospitalizationType { get; set; }
        public virtual ICollection<HospitalResponseBranch> HospitalResponseBranches { get; set; }
        public virtual ICollection<HospitalResponseProcess> HospitalResponseProcesses { get; set; }
     
    }
}