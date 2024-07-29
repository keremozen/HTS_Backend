using System.ComponentModel.DataAnnotations;
using HTS.Dto.Patient;
using HTS.Dto.TreatmentProcessStatus;
using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.PatientTreatmentProcess;

public class FinalizePtpDto
{
    [Required]
    public int FinalizationTypeId { get; set; }
    [Required]
    public string Description { get; set; }
}