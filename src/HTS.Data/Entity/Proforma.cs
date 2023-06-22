using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class Proforma : AuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public int OperationId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public int ProformaStatusId { get; set; }
        [Required]
        public decimal ExchangeRate { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string TPDescription { get; set; }
        [Required]
        public string ProformaCode { get; set; }
        [Required]
        public int Version { get; set; }
        [Required]
        public decimal TotalProformaPrice { get; set; }

        public int? RejectReasonId { get; set; }

        [ForeignKey("OperationId")]
        public Operation Operation { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        [ForeignKey("ProformaStatusId")]
        public ProformaStatus ProformaStatus { get; set; }

        [ForeignKey("RejectReasonId")]
        public virtual RejectReason? RejectReason { get; set; }
        public virtual ICollection<ProformaProcess> ProformaProcesses { get; set; }
        public virtual ICollection<ProformaAdditionalService> ProformaAdditionalServices { get; set; }
        public virtual ICollection<ProformaNotIncludingService> ProformaNotIncludingServices { get; set; }
    }
}
