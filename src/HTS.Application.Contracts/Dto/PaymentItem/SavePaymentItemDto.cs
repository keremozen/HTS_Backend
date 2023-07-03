using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.Currency;
using HTS.Dto.PaymentKind;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PaymentItem;

public class SavePaymentItemDto
{
    [Required]
    public int PaymentId { get; set; }
    [Required]
    public int PaymentKindId { get; set; }
    public string POSApproveCode { get; set; }
    public string Bank { get; set; }
    public string QueryNumber { get; set; }
    [Required]
    public int CurrencyId { get; set; }
    public decimal Price { get; set; }
    
}