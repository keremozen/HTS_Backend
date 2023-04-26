using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PatientDocumentStatus;

public class PatientDocumentStatusDto: EntityDto<int>
{
    public string Name { get; set; }
}