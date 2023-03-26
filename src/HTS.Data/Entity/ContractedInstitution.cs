using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace HTS.Data.Entity
{

    public class ContractedInstitution : FullAuditedEntity<int>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        
        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<ContractedInstitutionStaff> ContractedInstitutionStaff { get; set; }
    }
}
