using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class ProformaNotIncludingService : Entity<int>
    {
        [Required]
        public int ProformaId { get; set; }
        [Required]
        public string Description { get; set; }
        
        [ForeignKey("ProformaId")]
        public Proforma Proforma { get; set; }
    }
}
