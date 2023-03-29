using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Data.Entity
{

    public class ContractedInstitutionStaff : Entity<int>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int ContractedInstitutionId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [ForeignKey("ContractedInstitutionId")]
        public ContractedInstitution ContractedInstitution { get; set; }
    }
}
