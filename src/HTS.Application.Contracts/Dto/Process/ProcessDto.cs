using HTS.Dto.ProcessCost;
using HTS.Dto.ProcessRelation;
using HTS.Dto.ProcessType;
using System.Collections.Generic;
using HTS.Dto.ProcessKind;
using HTS.Enum;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Process;

public class ProcessDto : EntityDto<int>
{
    public string Name { get; set; }
    public string EnglishName { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public EntityEnum.ProcessTypeEnum ProcessTypeId { get; set; }
    public int? ProcessKindId { get; set; }
    public bool IsActive { get; set; }
    public ProcessTypeDto ProcessType { get; set; }
    public ProcessKindDto ProcessKind { get; set; }
    public List<ProcessCostDto> ProcessCosts { get; set; }
    public List<ProcessRelationDto> ProcessRelations { get; set; }
}