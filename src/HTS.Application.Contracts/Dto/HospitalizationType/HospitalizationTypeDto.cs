using Volo.Abp.Application.Dtos;

namespace HTS.Dto.HospitalizationType;

public class HospitalizationTypeDto :EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}