using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Volo.Abp.Domain.Entities.Auditing;

namespace HTS.Data.Entity
{

    public class Hospital : FullAuditedEntity<int>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        public int? PhoneCountryCodeId { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [ForeignKey("PhoneCountryCodeId")]
        public Nationality? PhoneCountryCode { get; set; }
        public virtual ICollection<HospitalStaff> HospitalStaffs { get; set; }
    }
}
