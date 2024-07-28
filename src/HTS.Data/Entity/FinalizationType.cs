using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class FinalizationType : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        public virtual ICollection<PatientTreatmentProcess> PatientTreatmentProcesses { get; set; }
    }
}
