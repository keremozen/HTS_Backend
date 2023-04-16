using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProcessRelation;

public class ProcessRelationDto : EntityDto<int>
{
    public int ProcessId { get; set; }
    public int ChildProcessId { get; set; }
}