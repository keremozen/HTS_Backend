using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.HospitalInterpreter;

public class HospitalInterpreterDto : EntityDto<int>
{
    public IdentityUserDto User { get; set; }
    public Guid UserId { get; set; }
    public int HospitalId { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
}