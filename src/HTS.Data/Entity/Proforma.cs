﻿using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class Proforma : AuditedEntityWithUser<int,IdentityUser>
    {
        [Required]
        public int OperationId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public decimal CurrencyAmount { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string TPDescription { get; set; }
        [Required]
        public int Version { get; set; }
        [Required]
        public decimal TotalProformaAmount { get; set; }
        
        [ForeignKey("OperationId")]
        public Operation Operation { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        public virtual ICollection<ProformaProcess> ProformaProcesses { get; set; }
        public virtual ICollection<ProformaAdditionalService> ProformaAdditionalServices { get; set; }
        public virtual ICollection<ProformaNotIncludingService> ProformaNotIncludingServices { get; set; }
    }
}