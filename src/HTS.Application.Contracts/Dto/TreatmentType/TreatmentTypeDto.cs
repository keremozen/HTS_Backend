using Volo.Abp.Application.Dtos;

namespace HTS.Dto.TreatmentType;

public class TreatmentTypeDto : EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}