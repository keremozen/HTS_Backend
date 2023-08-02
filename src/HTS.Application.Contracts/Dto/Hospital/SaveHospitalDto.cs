using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HTS.Dto.HospitalUHBStaff;

namespace HTS.Dto.Hospital;

public class SaveHospitalDto
{
    [Required, StringLength(50)]
    public string Name { get; set; }
    [Required, StringLength(50)]
    public string Code { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public int CityId { get; set; }
    [StringLength(20)]
    public string PhoneNumber { get; set; }
    public int? PhoneCountryCodeId { get; set; }
    [StringLength(50)]
    public string Email { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public List<SaveHospitalUHBStaffDto> HospitalUHBStaffs { get; set; }
}