using Volo.Abp.Application.Dtos;

namespace HTS.Dto.RejectReason;

public class RejectReasonDto: EntityDto<int>
{
    public string Reason { get; set; }
    public bool IsActive { get; set; }
}