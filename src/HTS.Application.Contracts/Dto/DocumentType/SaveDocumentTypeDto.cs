using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.DocumentType;

public class SaveDocumentTypeDto
{
    [Required, StringLength(50)]
    public string Name { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public bool IsActive { get; set; }
}