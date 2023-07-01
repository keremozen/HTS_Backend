using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PaymentReason;

public class PaymentReasonDto: EntityDto<int>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}