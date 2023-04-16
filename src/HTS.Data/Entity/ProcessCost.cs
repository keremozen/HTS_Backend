using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class ProcessCost : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public int ProcessId { get; set; }
        [Required]
        public DateTime ValidityStartDate { get; set; }
        [Required]
        public DateTime ValidityEndDate { get; set; }
        [Required]
        public int HospitalPrice { get; set; }
        [Required]
        public int UshasPrice { get; set; }
        [Required]
        public bool IsActive { get; set; }
        
        [ForeignKey("ProcessId")]
        public Process Process { get; set; }
     
     
     
    }
}