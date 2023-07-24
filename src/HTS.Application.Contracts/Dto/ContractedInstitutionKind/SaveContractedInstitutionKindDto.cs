using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.ContractedInstitutionKind;

public class SaveContractedInstitutionKindDto
{
    [Required]
    public string Name { get; set; }
    public bool IsActive { get; set; }
}