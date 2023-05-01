using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.HospitalResponseBranch
{
    public class SaveHospitalResponseBranchDto 
    {
        [Required]
        public int HospitalResponseId { get; set; }
        [Required]
        public int BranchId { get; set; }
    }
}