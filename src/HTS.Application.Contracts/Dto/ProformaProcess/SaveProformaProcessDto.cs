using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.Process;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProformaProcess;

public class SaveProformaProcessDto
{
    [Required]
    public int ProformaId { get; set; }
    [Required]
    public int ProcessId { get; set; }
    [Required]
    public int TreatmentCount { get; set; }
    [Required]
    public decimal PieceAmount { get; set; }
    [Required]
    public decimal TotalAmount { get; set; }
    [Required]
    public decimal ProformaAmount { get; set; }
    [Required]
    public int Change { get; set; }
    [Required]
    public decimal ProformaFinalAmount { get; set; }
}