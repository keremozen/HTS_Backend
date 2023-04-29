using Volo.Abp.Application.Dtos;

namespace HTS.Dto.HospitalConsultationStatus;

public class HospitalConsultationStatusDto: EntityDto<int>
{
    public string Name { get; set; }
}