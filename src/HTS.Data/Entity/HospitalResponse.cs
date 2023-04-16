using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalResponse : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public string Response { get; set; }
        [Required]
        public bool IsEvaluatable { get; set; }
        [Required]
        public bool IsActive { get; set; }
     
    }
}