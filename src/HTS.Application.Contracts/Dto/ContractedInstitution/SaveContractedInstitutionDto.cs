using System.ComponentModel.DataAnnotations;

public class SaveContractedInstitutionDto
{
    [Required, StringLength(50)]
    public string Name { get; set; }

    [Required, StringLength(500)]
    public string Description { get; set; }
    public bool IsActive { get; set; }
}