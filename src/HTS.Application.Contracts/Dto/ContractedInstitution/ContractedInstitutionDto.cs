using Volo.Abp.Application.Dtos;

public class ContractedInstitutionDto: EntityDto<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}