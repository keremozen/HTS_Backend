using HTS.Dto.Process;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProformaProcess;

public class ProformaProcessDto: EntityDto<int>
{
    public int ProformaId { get; set; }
    public int ProcessId { get; set; }
    public int TreatmentCount { get; set; }
    public decimal PieceAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal ProformaAmount { get; set; }
    public int Change { get; set; }
    public decimal ProformaFinalAmount { get; set; }
    public ProcessDto Process { get; set; }
}