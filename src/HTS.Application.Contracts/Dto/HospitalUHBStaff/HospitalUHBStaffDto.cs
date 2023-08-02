using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.HospitalUHBStaff;

public class HospitalUHBStaffDto : EntityDto<int>
{
    public int HospitalId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
}