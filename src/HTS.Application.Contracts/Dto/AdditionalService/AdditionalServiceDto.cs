using Volo.Abp.Application.Dtos;

namespace HTS.Dto.AdditionalService;

public class AdditionalServiceDto : EntityDto<int>
{
    public string Name { get; set; }
    public string EnglishName { get; set; }
    public bool Day { get; set; }
    public bool Piece { get; set; }
    public bool RoomType { get; set; }
    public bool Companion { get; set; }
}