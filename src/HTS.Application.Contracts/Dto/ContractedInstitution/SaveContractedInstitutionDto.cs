using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.ContractedInstitution;

public class SaveContractedInstitutionDto
{
    [Required, StringLength(50)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
        
    [Required]
    public bool IsActive { get; set; }
}