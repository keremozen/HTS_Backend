using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Data.Entity
{

    public class PatientNote : AuditedEntity<int>
    {
        [Required]
        public string Note { get; set; }
        
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int PatientNoteStatusId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [ForeignKey("PatientNoteStatusId")]
        public PatientNoteStatus PatientNoteStatus { get; set; }

    }
}
