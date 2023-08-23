using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class Payment : AuditedEntityWithUser<int, IdentityUser>
    {
        public int? ProformaId { get; set; }
        [Required]
        public int PtpId { get; set; }
        [Required]
        public int HospitalId { get; set; }
        [Required]
        public int RowNumber { get; set; }
        [Required]
        public string GeneratedRowNumber { get; set; }
        [Required]
        public string PatientNameSurname { get; set; }
        [Required]
        public string PayerNameSurname { get; set; }
        [Required]
        public string CollectorNameSurname { get; set; }
        [Required]
        public int PaymentReasonId { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        public string? ProcessingNumber { get; set; }
        public string? FileNumber { get; set; }
        public string? Description { get; set; }
        [Required]
        public string ProformaNumber { get; set; }
        [Required]
        public int PaymentStatusId { get; set; }
        
        [ForeignKey("PtpId")]
        public PatientTreatmentProcess PatientTreatmentProcess { get; set; }
        [ForeignKey("ProformaId")]
        public Proforma Proforma { get; set; }
        [ForeignKey("HospitalId")]
        public Hospital Hospital { get; set; }
        [ForeignKey("PaymentReasonId")]
        public PaymentReason PaymentReason { get; set; }

        [ForeignKey("PaymentStatusId")]
        public PaymentStatus PaymentStatus { get; set; }
        public virtual ICollection<PaymentItem> PaymentItems { get; set; }
        public virtual ICollection<PaymentDocument> PaymentDocuments { get; set; }

    }
}
