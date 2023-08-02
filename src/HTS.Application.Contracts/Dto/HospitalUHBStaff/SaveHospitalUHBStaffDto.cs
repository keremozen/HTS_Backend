using System;
using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.HospitalUHBStaff;

public class SaveHospitalUHBStaffDto
{
    [Required]
    public int HospitalId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Email { get; set; }
}