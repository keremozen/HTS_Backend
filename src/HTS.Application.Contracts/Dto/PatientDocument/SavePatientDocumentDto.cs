using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.PatientDocument;

public class SavePatientDocumentDto
{
    [Required]
    public int PatientId { get; set; }
    [Required]
    public int DocumentTypeId { get; set; }
    [Required]
    public int PatientDocumentStatusId { get; set; }
    [Required]
    public string Description { get; set; }

    [Required, StringLength(100)]
    public string FileName { get; set; }
    [Required]
    public string FilePath { get; set; }
}