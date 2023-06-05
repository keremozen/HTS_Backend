using System;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProcessCost;

public class ProcessCostDto : EntityDto<int>
{
    public int ProcessId { get; set; }
    public DateTime ValidityStartDate { get; set; }
    public DateTime ValidityEndDate { get; set; }
    public int HospitalPrice { get; set; }
    public int UshasPrice { get; set; }
    public bool IsActive { get; set; }
}