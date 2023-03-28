using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ContractedInstitution;

public class ContractedInstitutionDto: EntityDto<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}