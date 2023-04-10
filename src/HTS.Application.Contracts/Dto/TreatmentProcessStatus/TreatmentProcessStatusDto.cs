using Volo.Abp.Application.Dtos;

namespace HTS.Dto.TreatmentProcessStatus;

public class TreatmentProcessStatusDto: EntityDto<int>
{
    public string Name { get; set; }
}