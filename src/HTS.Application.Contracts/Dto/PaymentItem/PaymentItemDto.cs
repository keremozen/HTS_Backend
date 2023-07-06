using HTS.Dto.Currency;
using HTS.Dto.PaymentKind;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PaymentItem;

public class PaymentItemDto: EntityDto<int>
{
    public int PaymentId { get; set; }
    public int PaymentKindId { get; set; }
    public string POSApproveCode { get; set; }
    public string Bank { get; set; }
    public string QueryNumber { get; set; }
    public int CurrencyId { get; set; }
    public decimal Price { get; set; }
    public decimal ExchangeRate { get; set; }
    public CurrencyDto Currency { get; set; }
    public PaymentKindDto PaymentKind { get; set; }
}