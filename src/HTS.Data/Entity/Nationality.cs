using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class Nationality : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(10)]
        public string PhoneCode { get; set; }
        [Required, StringLength(10)]
        public string CountryCode { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
