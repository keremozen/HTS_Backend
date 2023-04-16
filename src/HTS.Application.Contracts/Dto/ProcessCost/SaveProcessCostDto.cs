using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.ProcessCost;

public class SaveProcessCostDto
{
    [Required]
    public int ProcessId { get; set; }
    [Required]
    public DateTime ValidityStartDate { get; set; }
    [Required]
    public DateTime ValidityEndDate { get; set; }
    [Required]
    public int HospitalPrice { get; set; }
    [Required]
    public int UshasPrice { get; set; }
    [Required]
    public bool IsActive { get; set; }
}