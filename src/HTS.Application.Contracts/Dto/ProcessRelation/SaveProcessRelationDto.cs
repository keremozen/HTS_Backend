using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.ProcessRelation;

public class SaveProcessRelationDto
{
    [Required]
    public int ProcessId { get; set; }
    [Required]
    public int ChildProcessId { get; set; }
}