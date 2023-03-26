using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Nationality;

public class NationalityDto: EntityDto<int>
{
    public string Name { get; set; }
    public string PhoneCode { get; set; }
    public bool IsActive { get; set; }
}