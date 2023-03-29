using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.ContractedInstitutionStaff;

public class SaveContractedInstitutionStaffDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public bool IsActive { get; set; }
}