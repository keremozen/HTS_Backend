using Volo.Abp.Application.Dtos;

namespace HTS.Dto.OperationStatus;

public class OperationStatusDto: EntityDto<int>
{
    public string Name { get; set; }
}