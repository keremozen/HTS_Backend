using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.Material;

public class SaveMaterialDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public bool IsActive { get; set; }
}