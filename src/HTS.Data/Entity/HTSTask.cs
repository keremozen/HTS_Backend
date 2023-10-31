using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class HTSTask : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int TaskTypeId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int RelatedEntityId { get; set; }
        [ForeignKey("TaskTypeId")]
        public TaskType TaskType { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
