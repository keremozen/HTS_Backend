using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Data.Entity
{

    public class DocumentType : FullAuditedEntity, IPassivable
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public bool IsActive { get; set; }
     
    }
}
