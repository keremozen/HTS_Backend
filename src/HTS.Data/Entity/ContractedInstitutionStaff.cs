using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace HTS.Data.Entity
{

    public class ContractedInstitutionStaff : FullAuditedEntity<int>
    {
        [Required, StringLength(500)]
        public string NameSurname { get; set; }

        [Required, StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public ContractedInstitution ContractedInstitution { get; set; }
        public Nationality PhoneCountryCode { get; set; }

    }
}
