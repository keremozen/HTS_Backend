using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.HospitalResponseProcess
{
    public class SaveHospitalResponseProcessDto 
    {
        [Required]
        public int HospitalResponseId { get; set; }
        [Required]
        public int ProcessId { get; set; }
        [Required]
        public int Count { get; set; }
    }
}