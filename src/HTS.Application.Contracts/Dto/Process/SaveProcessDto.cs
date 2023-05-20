using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.ProcessCost;
using HTS.Dto.ProcessRelation;

namespace HTS.Dto.Process;

public class SaveProcessDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    public string Description { get; set; }
    public int ProcessTypeId { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public List<SaveProcessCostDto> ProcessCosts { get; set; }
    public List<SaveProcessRelationDto> ProcessRelations { get; set; }
}