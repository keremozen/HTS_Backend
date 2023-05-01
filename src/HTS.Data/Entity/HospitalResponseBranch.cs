using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalResponseBranch : Entity<int>
    {
        [Required]
        public int HospitalResponseId { get; set; }
        [Required]
        public int BranchId { get; set; }

        [ForeignKey("HospitalResponseId")]
        public HospitalResponse HospitalResponse { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
     
    }
}