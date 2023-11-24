using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Data.Entity
{

    public class InvitationLetterDocument : AuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public int SalesMethodAndCompanionInfoId { get; set; }

        [Required, StringLength(100)]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public string ContentType { get; set; }
        [ForeignKey("SalesMethodAndCompanionInfoId")]
        public SalesMethodAndCompanionInfo SalesMethodAndCompanionInfo { get; set; }
    }
}
