using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class ContractedInstitutionType : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
