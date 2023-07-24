using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.ContractedInstitutionType;

public class SaveContractedInstitutionTypeDto
{
    [Required]
    public string Name { get; set; }
    public bool IsActive { get; set; }
}