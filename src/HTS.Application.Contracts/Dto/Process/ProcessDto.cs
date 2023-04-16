using HTS.Dto.ProcessType;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Process;

public class ProcessDto : EntityDto<int>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public int ProcessTypeId { get; set; }
    public bool IsActive { get; set; }
    public ProcessTypeDto ProcessType { get; set; }
}