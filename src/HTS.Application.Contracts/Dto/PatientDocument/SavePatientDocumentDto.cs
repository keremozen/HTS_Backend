using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.PatientDocument;

public class SavePatientDocumentDto
{
    [Required]
    public int PatientId { get; set; }
    public int? DocumentTypeId { get; set; }
    public string Description { get; set; }
    [Required, StringLength(100)]
    public string FileName { get; set; }
    [Required]
    public string File { get; set; }
    [Required]
    public string ContentType { get; set; }
}