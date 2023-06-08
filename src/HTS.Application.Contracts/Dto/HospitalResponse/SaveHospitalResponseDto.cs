using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HTS.Dto.HospitalResponseBranch;
using HTS.Dto.HospitalResponseProcess;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.HospitalResponse;

public class SaveHospitalResponseDto
{
    public int? HospitalConsultationId { get; set; }
    public string Description { get; set; }
    [Required]
    public int HospitalResponseTypeId { get; set; }
    public int? HospitalizationTypeId { get; set; }

    public DateTime? PossibleTreatmentDate { get; set; }
    public int? HospitalizationNumber { get; set; }
    public virtual ICollection<SaveHospitalResponseBranchDto> HospitalResponseBranches { get; set; }
    public virtual ICollection<SaveHospitalResponseProcessDto> HospitalResponseProcesses { get; set; }

}