using System;
using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.HospitalPricer;

public class SaveHospitalPricerDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public int HospitalId { get; set; }
    [Required]
    public bool IsDefault { get; set; }
    [Required]
    public bool IsActive { get; set; }
}