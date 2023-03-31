using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.ContractedInstitutionStaff;

public class ContractedInstitutionStaffDto: EntityDto<int>
{
    public string NameSurname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int ContractedInstitutionId { get; set; }
    public int PhoneCountryCodeId { get; set; }
    public bool IsActive { get; set; }
}