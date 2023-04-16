using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Major;

public class MajorDto: EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}