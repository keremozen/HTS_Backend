using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace HTS.Data.Entity
{

    public class PatientTreatmentProcess : FullAuditedEntity<int>
    {
        [Required]
        public string TreatmentCode { get; set; }
        public Patient Patient { get; set; }
        public TreatmentProcessStatus TreatmentProcessStatus { get; set; }
        public virtual ICollection<SalesMethodAndCompanionInfo> SalesMethodAndCompanionInfos { get; set; }


    }
}
