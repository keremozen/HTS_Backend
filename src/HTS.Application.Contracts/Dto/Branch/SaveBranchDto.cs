using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.Branch;

public class SaveBranchDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public bool IsActive { get; set; }
}