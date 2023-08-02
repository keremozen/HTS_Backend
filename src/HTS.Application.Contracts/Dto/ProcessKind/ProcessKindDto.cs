using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProcessKind;

public class ProcessKindDto: EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}