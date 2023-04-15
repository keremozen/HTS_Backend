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
}