using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Data.Entity
{

    public class PatientDocument : AuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public int PatientId { get; set; }
        public int? DocumentTypeId { get; set; }
        [Required]
        public int PatientDocumentStatusId { get; set; }
        public string? Description { get; set; }
        [Required, StringLength(100)]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("DocumentTypeId")]
        public DocumentType? DocumentType { get; set; }
        [ForeignKey("PatientDocumentStatusId")]
        public PatientDocumentStatus PatientDocumentStatus { get; set; }

    }
}
