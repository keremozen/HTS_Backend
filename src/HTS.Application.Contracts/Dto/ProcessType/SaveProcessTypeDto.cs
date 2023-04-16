using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.ProcessType;

public class SaveProcessTypeDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public bool IsActive { get; set; }
}