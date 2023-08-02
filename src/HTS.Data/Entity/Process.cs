using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class Process : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string EnglishName { get; set; }
        [Required]
        public string Code { get; set; }
        public string? Description { get; set; }
        [Required]
        public int ProcessTypeId { get; set; }
        public int? ProcessKindId { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("ProcessTypeId")]
        public ProcessType ProcessType { get; set; }

        [ForeignKey("ProcessKindId")]
        public ProcessKind ProcessKind { get; set; }

        public virtual ICollection<ProcessCost> ProcessCosts { get; set; }
        [InverseProperty("Process")]
        public virtual ICollection<ProcessRelation> ProcessRelations { get; set; }
        [InverseProperty("ChildProcess")]
        public virtual ICollection<ProcessRelation> ProcessRelationChildren { get; set; }
    }
}