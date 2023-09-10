using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.Nationality;

public class SaveExchangeRateInformationDto
{

    [Required]
    public int CurrencyId { get; set; }
    [Required]
    public decimal ExchangeRate { get; set; }
}