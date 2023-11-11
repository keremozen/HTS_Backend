using Volo.Abp.Application.Dtos;

namespace HTS.Dto.TaskType;

public class TaskTypeDto: EntityDto<int>
{
    public string Name { get; set; }
}