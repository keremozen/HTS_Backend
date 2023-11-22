using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalAgentNote : AuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public string Note { get; set; }
        
        [Required]
        public int HospitalResponseId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("HospitalResponseId")]
        public HospitalResponse HospitalResponse { get; set; }
        [ForeignKey("StatusId")]
        public HospitalAgentNoteStatus Status { get; set; }
    }
}
