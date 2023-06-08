using System;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProcessCost;

public class ProcessCostDto : EntityDto<int>
{
    public int ProcessId { get; set; }
    public DateTime ValidityStartDate { get; set; }
    public DateTime ValidityEndDate { get; set; }
    public decimal HospitalPrice { get; set; }
    public decimal UshasPrice { get; set; }
    public bool IsActive { get; set; }
}