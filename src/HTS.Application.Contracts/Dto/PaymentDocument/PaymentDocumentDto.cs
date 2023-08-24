using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PaymentDocument;

public class PaymentDocumentDto: EntityDto<int>
{
    public int PaymentId { get; set; }
    public string FileName { get; set; }
    public string File { get; set; }
    public string ContentType { get; set; }
}