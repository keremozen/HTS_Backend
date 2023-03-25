using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace HTS.Data.Entity
{

    public class PatientNote : AuditedEntity<int>
    {
        [Required]
        public string Note { get; set; }
        public Patient Patient { get; set; }
        public PatientNoteStatus PatientNoteStatus { get; set; }

    }
}
