using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.HospitalResponseMaterial
{
    public class SaveHospitalResponseMaterialDto 
    {
        [Required]
        public int HospitalResponseId { get; set; }
        [Required]
        public int MaterialId { get; set; }
    }
}