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
    public decimal UnitPrice { get; set; }
    [Required]
    public decimal TotalPrice { get; set; }
    [Required]
    public decimal ProformaPrice { get; set; }
    [Required]
    public int Change { get; set; }
    [Required]
    public decimal ProformaFinalPrice { get; set; }
}