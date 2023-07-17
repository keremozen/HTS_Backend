using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.PaymentDocument;

public class SavePaymentDocumentDto
{
    [Required]
    public int PaymentId { get; set; }
    [Required]
    public string SignedFileName { get; set; }
    [Required]
    public string SignedFile { get; set; }
}