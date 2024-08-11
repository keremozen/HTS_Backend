using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.HospitalResponse;

namespace HTS.Dto.Operation;

public class SaveOperationDto
{
    [Required]
    public int HospitalResponseId { get; set; }
    public int TreatmentTypeId { get; set; }
    public int? PatientTreatmentProcessId { get; set; }
    public int? HospitalId { get; set; }
    public SaveHospitalResponseDto HospitalResponse { get; set; }
}