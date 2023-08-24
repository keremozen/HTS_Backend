using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Data.Entity
{

    public class HospitalConsultationDocument : AuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public int HospitalConsultationId { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
        [Required]
        public int PatientDocumentStatusId { get; set; }
        [Required]
        public string Description { get; set; }

        [Required, StringLength(100)]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public string ContentType { get; set; }
        [ForeignKey("HospitalConsultationId")]
        public HospitalConsultation HospitalConsultation { get; set; }
        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }
        [ForeignKey("PatientDocumentStatusId")]
        public PatientDocumentStatus PatientDocumentStatus { get; set; }

    }
}
