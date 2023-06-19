using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.AdditionalService;
using HTS.Enum;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProformaAdditionalService;

public class SaveProformaAdditionalServiceDto
{
    [Required]
    public int ProformaId { get; set; }
    [Required]
    public EntityEnum.AdditionalServiceEnum AdditionalServiceId { get; set; }
    public int? DayCount { get; set; }
    public EntityEnum.RoomTypeEnum? RoomTypeId { get; set; }
    public int? CompanionCount { get; set; }
    public int? ItemCount { get; set; }
}