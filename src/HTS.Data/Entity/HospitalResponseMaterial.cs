using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalResponseMaterial : Entity<int>
    {
        [Required]
        public int HospitalResponseId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public int Amount { get; set; }
        [ForeignKey("HospitalResponseId")]
        public HospitalResponse HospitalResponse { get; set; }
        [ForeignKey("MaterialId")]
        public Material Material { get; set; }
    }
}