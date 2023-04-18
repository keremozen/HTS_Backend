using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Branch;

public class BranchDto: EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}