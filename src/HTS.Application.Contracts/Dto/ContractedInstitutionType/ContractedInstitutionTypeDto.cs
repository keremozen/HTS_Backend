using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ContractedInstitutionType;

public class ContractedInstitutionTypeDto : EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}