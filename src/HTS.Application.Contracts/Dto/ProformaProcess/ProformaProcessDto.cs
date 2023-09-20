using HTS.Dto.Process;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProformaProcess;

public class ProformaProcessDto: EntityDto<int>
{
    public int ProformaId { get; set; }
    public int ProcessId { get; set; }
    public int TreatmentCount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal ProformaPrice { get; set; }
    public decimal Change { get; set; }
    public decimal ProformaFinalPrice { get; set; }
    public ProcessDto Process { get; set; }
}