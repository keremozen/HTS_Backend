using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class Hospital : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string Code { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int CityId { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        public int? PhoneCountryCodeId { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [ForeignKey("PhoneCountryCodeId")]
        public Nationality? PhoneCountryCode { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public virtual ICollection<HospitalStaff> HospitalStaffs { get; set; }
    }
}
