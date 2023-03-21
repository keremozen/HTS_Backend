using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace HTS.Data.Entity
{

    public class DocumentType : FullAuditedEntity<int>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public bool IsActive { get; set; }
     
    }
}
