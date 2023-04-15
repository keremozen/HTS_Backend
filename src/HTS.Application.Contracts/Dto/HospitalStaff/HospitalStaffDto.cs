using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.HospitalStaff;

public class HospitalStaffDto : EntityDto<int>
{
    public IdentityUserDto User { get; set; }
    public int HospitalId { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
}