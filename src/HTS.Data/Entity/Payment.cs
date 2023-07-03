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
        [Required]
        public int? ProformaId { get; set; }
        [Required]
        public int PtpId { get; set; }
        [Required]
        public int HospitalId { get; set; }
        [Required]
        public string RowNumber { get; set; }
        [Required]
        public string PatientNameSurname { get; set; }
        [Required]
        public string PaidNameSurname { get; set; }
        [Required]
        public string ProcessingUserNameSurname { get; set; }
        [Required]
        public int PaymentReasonId { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        public string? ProcessingNumber { get; set; }
        public string? FileNumber { get; set; }
        public string? Description { get; set; }
        
        [ForeignKey("PtpId")]
        public PatientTreatmentProcess PatientTreatmentProcess { get; set; }
        [ForeignKey("ProformaId")]
        public Proforma Proforma { get; set; }
        [ForeignKey("HospitalId")]
        public Hospital Hospital { get; set; }
        [ForeignKey("PaymentReasonId")]
        public PaymentReason PaymentReason { get; set; }
        
        public virtual ICollection<PaymentItem> PaymentItems { get; set; }
    }
}
