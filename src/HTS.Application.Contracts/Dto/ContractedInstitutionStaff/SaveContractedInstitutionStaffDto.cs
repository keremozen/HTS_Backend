using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.ContractedInstitutionStaff;

public class SaveContractedInstitutionStaffDto
{
    [Required, StringLength(500)]
    public string NameSurname { get; set; }
    [Required, StringLength(20)]
    public string PhoneNumber { get; set; }
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public int ContractedInstitutionId { get; set; }
    [Required]
    public int PhoneCountryCodeId { get; set; }
    [Required]
    public bool IsActive { get; set; }
}