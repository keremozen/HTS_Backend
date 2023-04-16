using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class ProcessRelation : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public int ProcessId { get; set; }
        [Required]
        public int ChildProcessId { get; set; }
        [ForeignKey("ProcessId")]
        public Process Process { get; set; }
        [ForeignKey("ChildProcessId")]
        public Process ChildProcess { get; set; }
     
    }
}