using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class Patient : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string? PassportNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        [StringLength(50), EmailAddress]
        public string? Email { get; set; }

        public int? PhoneCountryCodeId { get; set; }
        public int NationalityId { get; set; }
        public int? GenderId { get; set; }
        public int? MotherTongueId { get; set; }
        public int? SecondTongueId { get; set; }
        public bool? IsAssignedToTik { get; set; }
        public Guid? UserIdAssignedToTik { get; set; }
        public DateTime? TikAssignmentDate { get; set; }
        public Guid? TikUserIdReturned { get; set; }
        public DateTime? TikReturnDate { get; set; }
        public bool? NoTreatmentPlan { get; set; }

        [ForeignKey("PhoneCountryCodeId")]
        public Nationality? PhoneCountryCode { get; set; }
        
        [ForeignKey("NationalityId")]
        public Nationality Nationality { get; set; }
        
        [ForeignKey("GenderId")]
        public Gender? Gender { get; set; }
        
        [ForeignKey("MotherTongueId")]
        public Language? MotherTongue { get; set; }
        
        [ForeignKey("SecondTongueId")]
        public Language? SecondTongue { get; set; }

        [ForeignKey("UserIdAssignedToTik")]
        public IdentityUser? UserAssignedToTik { get; set; }

        [ForeignKey("TikUserIdReturned")]
        public IdentityUser? TikUserReturned { get; set; }

        public virtual ICollection<PatientNote> PatientNotes { get; set; }
        public virtual ICollection<PatientDocument> PatientDocuments { get; set; }
        public virtual ICollection<PatientTreatmentProcess> PatientTreatmentProcesses { get; set; }

   
    }
}
