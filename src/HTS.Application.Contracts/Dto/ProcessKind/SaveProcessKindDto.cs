using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.ProcessKind;

public class SaveProcessKindDto
{
    [Required]
    public string Name { get; set; }
    public bool IsActive { get; set; }
}