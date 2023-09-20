using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class ProformaProcess : Entity<int>
    {
        [Required]
        public int ProformaId { get; set; }
        [Required]
        public int ProcessId { get; set; }
        [Required]
        public int TreatmentCount { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public decimal ProformaPrice { get; set; }
        [Required]
        public decimal Change { get; set; }
        [Required]
        public decimal ProformaFinalPrice { get; set; }
        
        [ForeignKey("ProformaId")]
        public Proforma Proforma { get; set; }
        [ForeignKey("ProcessId")]
        public Process Process { get; set; }
    }
}
