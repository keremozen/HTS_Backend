using HTS.Dto.AdditionalService;
using HTS.Enum;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProformaAdditionalService;

public class ProformaAdditionalServiceDto : EntityDto<int>
{
    public int ProformaId { get; set; }
    public EntityEnum.AdditionalServiceEnum AdditionalServiceId { get; set; }
    public int? DayCount { get; set; }
    public EntityEnum.RoomTypeEnum? RoomTypeId { get; set; }
    public int? CompanionCount { get; set; }
    public int? ItemCount { get; set; }
    public AdditionalServiceDto AdditionalService { get; set; }

}