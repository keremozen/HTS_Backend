using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.TreatmentType;

public class SaveTreatmentTypeDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public bool IsActive { get; set; }
}