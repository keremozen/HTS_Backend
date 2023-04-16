using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.Process;

public class SaveProcessDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    public string Description { get; set; }
    public int ProcessTypeId { get; set; }
    [Required]
    public bool IsActive { get; set; }
}