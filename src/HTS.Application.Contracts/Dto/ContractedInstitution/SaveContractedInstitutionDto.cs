using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.ContractedInstitution;

public class SaveContractedInstitutionDto
{
    [Required, StringLength(50)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [StringLength(50), EmailAddress]
    public string Email { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }
    public int PhoneCountryCodeId { get; set; }
    public int NationalityId { get; set; }

    [StringLength(500)]
    public string Site { get; set; }

    [StringLength(500)]
    public string Address { get; set; }

    [Required]
    public bool IsActive { get; set; }
}