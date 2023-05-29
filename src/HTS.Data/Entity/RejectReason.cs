using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class RejectReason : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public string Reason { get; set; }
        [Required]
        public bool IsActive { get; set; }
     
    }
}