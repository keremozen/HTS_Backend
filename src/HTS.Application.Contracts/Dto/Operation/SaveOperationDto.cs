using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.Operation;

public class SaveOperationDto
{
    [Required]
    public int HospitalResponseId { get; set; }
    public DateTime? TravelDateToTurkey { get; set; }
    public DateTime? TreatmentDate { get; set; }
    public int? TreatmentTypeId { get; set; }
    public bool? AnyInvitationLetter { get; set; }
    public Guid? AppointedInterpreterId { get; set; }
}