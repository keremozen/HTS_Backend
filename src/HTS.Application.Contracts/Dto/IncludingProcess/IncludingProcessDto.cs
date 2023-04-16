using Volo.Abp.Application.Dtos;

namespace HTS.Dto.IncludingProcess;

public class IncludingProcessDto : EntityDto<int>
{
    public int ProcessId { get; set; }
    public int ChildProcessId { get; set; }
}