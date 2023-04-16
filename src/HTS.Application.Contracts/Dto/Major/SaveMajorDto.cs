using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.Major;

public class SaveMajorDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public bool IsActive { get; set; }
}