using HTS.Dto.AdditionalService;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProformaAdditionalService;

public class ProformaAdditionalServiceDto : EntityDto<int>
{
    public int ProformaId { get; set; }
    public int AdditionalServiceId { get; set; }
    public int? DayCount { get; set; }
    public int? RoomTypeId { get; set; }
    public int? CompanionCount { get; set; }
    public int? ItemCount { get; set; }
    public AdditionalServiceDto AdditionalService { get; set; }
}