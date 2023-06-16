using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ProformaNotIncludingService;

public class SaveProformaNotIncludingServiceDto 
{
    [Required]
    public int ProformaId { get; set; }
    [Required]
    public string Description { get; set; }
}