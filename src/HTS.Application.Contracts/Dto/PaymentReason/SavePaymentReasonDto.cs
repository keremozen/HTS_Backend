using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.PaymentReason;

public class SavePaymentReasonDto
{
    [Required]
    public string Name { get; set; }
    public bool IsActive { get; set; }
}