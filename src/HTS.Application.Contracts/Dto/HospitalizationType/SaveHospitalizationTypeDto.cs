using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.HospitalizationType;

public class SaveHospitalizationTypeDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public bool IsActive { get; set; }
}