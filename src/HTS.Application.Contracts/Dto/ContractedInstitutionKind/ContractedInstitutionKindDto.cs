using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ContractedInstitutionKind;

public class ContractedInstitutionKindDto: EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}