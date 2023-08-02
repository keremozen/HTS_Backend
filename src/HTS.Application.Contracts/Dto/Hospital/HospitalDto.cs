using System.Collections.Generic;
using HTS.Dto.City;
using HTS.Dto.HospitalStaff;
using HTS.Dto.HospitalUHBStaff;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Hospital;

public class HospitalDto : EntityDto<int>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Address { get; set; }
    public int CityId { get; set; }
    public string PhoneNumber { get; set; }
    public int? PhoneCountryCodeId { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public NationalityDto PhoneCountryCode { get; set; }
    public CityDto City { get; set; }
    public List<HospitalStaffDto> HospitalStaffs { get; set; }
    public List<HospitalUHBStaffDto> HospitalUHBStaffs { get; set; }
}