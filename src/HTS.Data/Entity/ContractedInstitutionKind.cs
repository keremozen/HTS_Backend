using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class ContractedInstitutionKind : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
