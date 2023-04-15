using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Data.Entity
{

    public class ContractedInstitution : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50), EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        public int? PhoneCountryCodeId { get; set; }
        public int? NationalityId { get; set; }

        [StringLength(500)]
        public string? Site { get; set; }

        public string? Address { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [ForeignKey("PhoneCountryCodeId")]
        public Nationality? PhoneCountryCode { get; set; }

        [ForeignKey("NationalityId")]
        public Nationality? Nationality { get; set; }
        public virtual ICollection<ContractedInstitutionStaff>? ContractedInstitutionStaff { get; set; }
    }
}
