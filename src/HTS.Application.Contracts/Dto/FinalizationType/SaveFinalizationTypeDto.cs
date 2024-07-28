using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.FinalizationType;

public class SaveFinalizationTypeDto
{
    [Required]
    public string Name { get; set; }
}