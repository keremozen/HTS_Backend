using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.HospitalConsultationDocument;

public class SaveHospitalConsultationDocumentDto
{
    [Required]
    public int DocumentTypeId { get; set; }
    [Required]
    public int PatientDocumentStatusId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required, StringLength(100)]
    public string FileName { get; set; }
    [Required]
    public string File { get; set; }
    [Required]
    public string ContentType { get; set; }
}