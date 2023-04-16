using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProcessType;

public class ProcessTypeDto :EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}