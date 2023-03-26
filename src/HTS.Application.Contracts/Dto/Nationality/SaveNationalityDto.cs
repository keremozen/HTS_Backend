using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.Nationality;

public class SaveNationalityDto
{
    [Required, StringLength(50)]
    public string Name { get; set; }

    [Required, StringLength(10)]
    public string PhoneCode { get; set; }
    public bool IsActive { get; set; }
}