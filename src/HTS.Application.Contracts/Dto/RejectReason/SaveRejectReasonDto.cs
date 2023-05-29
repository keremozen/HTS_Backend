using System.ComponentModel.DataAnnotations;

namespace HTS.Dto.RejectReason;

public class SaveRejectReasonDto
{
    [Required]
    public string Reason { get; set; }
    [Required]
    public bool IsActive { get; set; }
}