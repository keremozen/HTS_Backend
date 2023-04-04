using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class PatientDocument : AuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public string Description { get; set; }

        [Required, StringLength(100)]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        public Patient Patient { get; set; }
        public DocumentType DocumentType { get; set; }
        public PatientDocumentStatus PatientDocumentStatus { get; set; }

    }
}
