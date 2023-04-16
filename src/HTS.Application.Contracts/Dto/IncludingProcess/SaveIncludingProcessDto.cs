using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.IncludingProcess;

public class SaveIncludingProcessDto
{
    [Required]
    public int ProcessId { get; set; }
    [Required]
    public int ChildProcessId { get; set; }
}