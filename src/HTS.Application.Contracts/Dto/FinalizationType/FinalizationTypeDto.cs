using Volo.Abp.Application.Dtos;

namespace HTS.Dto.FinalizationType;

public class FinalizationTypeDto: EntityDto<int>
{
    public string Name { get; set; }
}