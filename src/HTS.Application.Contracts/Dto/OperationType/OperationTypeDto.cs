using Volo.Abp.Application.Dtos;

namespace HTS.Dto.OperationType;

public class OperationTypeDto: EntityDto<int>
{
    public string Name { get; set; }
}