using Volo.Abp.Identity;

namespace HTS.Dto.HospitalStaff;

public class HospitalStaffDto
{
    public IdentityUserDto User { get; set; }
    public int HospitalId { get; set; }
    public bool IsActive { get; set; }
}