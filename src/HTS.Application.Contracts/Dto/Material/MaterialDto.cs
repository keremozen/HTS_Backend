using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Material;

public class MaterialDto: EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}