using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class ExchangeRateInformation : CreationAuditedEntity<int>
    {
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public decimal ExchangeRate { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
    }
}
