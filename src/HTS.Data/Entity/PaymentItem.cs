using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class PaymentItem : Entity<int>
    {
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public int PaymentKindId { get; set; }
        public string? POSApproveCode { get; set; }
        public string? Bank { get; set; }
        public string? QueryNumber { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        public decimal Price { get; set; }
        [Required]
        public decimal ExchangeRate { get; set; }

        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        [ForeignKey("PaymentKindId")]
        public PaymentKind PaymentKind { get; set; }
    }
}
