using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalResponse : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public int HospitalConsultationId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int HospitalResponseTypeId { get; set; }

        public DateTime? PossibleTreatmentDate { get; set; }
        public int? HospitalizationNumber { get; set; }
        [ForeignKey("HospitalResponseTypeId")]
        public HospitalResponseType HospitalResponseType { get; set; }

        [ForeignKey("HospitalConsultationId")]
        public HospitalConsultation HospitalConsultation { get; set; }

        public virtual ICollection<HospitalResponseBranch> HospitalResponseBranches { get; set; }
     
    }
}