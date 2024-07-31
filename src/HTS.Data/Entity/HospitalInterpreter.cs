using Volo.Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class HospitalInterpreter : Entity<int>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int HospitalId { get; set; }
        [Required]
        public bool IsDefault { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [ForeignKey("HospitalId")]
        public Hospital Hospital { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
