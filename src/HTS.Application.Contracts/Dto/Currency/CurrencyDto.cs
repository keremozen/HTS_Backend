using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Currency;

public class CurrencyDto : EntityDto<int>
{
    public string Name { get; set; }
}
