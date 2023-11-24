using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.InvitationLetterDocument;

public class SaveDocumentDto
{
    [Required]
    public int SalesMethodAndCompanionInfoId { get; set; }
    [Required, StringLength(100)]
    public string FileName { get; set; }
    [Required]
    public string File { get; set; }
    [Required]
    public string ContentType { get; set; }
}