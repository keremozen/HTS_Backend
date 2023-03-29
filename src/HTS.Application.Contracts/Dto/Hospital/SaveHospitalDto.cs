using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.Hospital;

public class SaveHospitalDto
{
    [Required, StringLength(50)]
    public string Name { get; set; }
    [StringLength(20)]
    public string PhoneNumber { get; set; }
    public int? PhoneCountryCodeId { get; set; }
    [StringLength(50)]
    public string Email { get; set; }
    [Required]
    public bool IsActive { get; set; }
}