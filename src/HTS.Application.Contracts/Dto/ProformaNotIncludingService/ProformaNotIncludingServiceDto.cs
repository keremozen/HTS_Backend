using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProformaNotIncludingService;

public class ProformaNotIncludingServiceDto : EntityDto<int>
{
    public string Description { get; set; }
    
}