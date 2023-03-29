using Volo.Abp.Identity;

namespace HTS.Dto.ContractedInstitutionStaff;

public class ContractedInstitutionStaffDto
{
    public IdentityUserDto User { get; set; }
    public int ContractedInstitutionId { get; set; }
    public bool IsActive { get; set; }
}