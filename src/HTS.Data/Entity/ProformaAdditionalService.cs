using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class ProformaAdditionalService : Entity<int>
    {
        [Required]
        public int ProformaId { get; set; }
        [Required]
        public int AdditionalServiceId { get; set; }
        public int? DayCount { get; set; }
        public int? RoomTypeId { get; set; }
        public int? CompanionCount { get; set; }
        public int? ItemCount { get; set; }
        
        [ForeignKey("ProformaId")]
        public Proforma Proforma { get; set; }
        [ForeignKey("AdditionalServiceId")]
        public AdditionalService AdditionalService { get; set; }
    }
}
