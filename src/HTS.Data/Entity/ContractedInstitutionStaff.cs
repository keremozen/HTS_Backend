using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class ContractedInstitutionStaff : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required, StringLength(500)]
        public string NameSurname { get; set; }
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }
        [Required]
        public int ContractedInstitutionId { get; set; }
        public int? PhoneCountryCodeId { get; set; }
        [Required]
        public bool IsDefault { get; set; }     
        [Required]
        public bool IsActive { get; set; }
        [ForeignKey("ContractedInstitutionId")]
        public ContractedInstitution ContractedInstitution { get; set; }
        [ForeignKey("PhoneCountryCodeId")]
        public Nationality? PhoneCountryCode { get; set; }
    }
}
