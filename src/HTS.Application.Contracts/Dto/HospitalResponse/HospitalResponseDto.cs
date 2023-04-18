using Volo.Abp.Application.Dtos;

namespace HTS.Dto.HospitalResponse;

public class HospitalResponseDto : EntityDto<int>
{
    public string Response { get; set; }
    public bool IsAssessable { get; set; }
    public bool IsActive { get; set; }
}