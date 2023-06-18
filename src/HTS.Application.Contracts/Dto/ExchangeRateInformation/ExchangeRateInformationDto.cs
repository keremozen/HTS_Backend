using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ExchangeRateInformation;

public class ExchangeRateInformationDto : CreationAuditedEntityDto<int>
{
    public int CurrencyId { get; set; }
    public decimal ExchangeRate { get; set; }
}