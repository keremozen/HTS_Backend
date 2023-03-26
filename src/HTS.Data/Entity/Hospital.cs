using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace HTS.Data.Entity
{

    public class Hospital : FullAuditedEntity<int>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }


        [Required, StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public Nationality PhoneCountryCode { get; set; }
    }
}
