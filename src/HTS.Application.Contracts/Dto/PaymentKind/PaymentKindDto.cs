using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PaymentKind;

public class PaymentKindDto : EntityDto<int>
{
    public string Name { get; set; }
}