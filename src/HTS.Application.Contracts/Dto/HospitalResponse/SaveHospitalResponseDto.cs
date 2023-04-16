using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.HospitalResponse;

public class SaveHospitalResponseDto
{
    [Required]
    public string Response { get; set; }
    [Required]
    public bool IsEvaluatable { get; set; }
    [Required]
    public bool IsActive { get; set; }
}