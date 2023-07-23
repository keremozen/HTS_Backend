using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.PaymentDocument;

public class SavePaymentDocumentDto
{
    [Required]
    public int PaymentId { get; set; }
    [Required]
    public string FileName { get; set; }
    [Required]
    public string File { get; set; }
}