using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.ContractedInstitutionStaff;

public class SaveListContractedInstitutionStaffDto
{
    [Required]
    public int ContractedInstitutionId { get; set; }
    public List<SaveContractedInstitutionStaffDto> SaveContractedInstitutionStaffs { get; set; }
}