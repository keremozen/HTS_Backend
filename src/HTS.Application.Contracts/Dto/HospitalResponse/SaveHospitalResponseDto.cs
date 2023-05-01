using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HTS.Dto.HospitalResponseBranch;
using HTS.Dto.HospitalResponseMaterial;
using HTS.Dto.HospitalResponseProcess;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.HospitalResponse;

public class SaveHospitalResponseDto
{
    [Required]
    public int HospitalConsultationId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int HospitalResponseTypeId { get; set; }

    public DateTime? PossibleTreatmentDate { get; set; }
    public int? HospitalizationNumber { get; set; }
    public HospitalResponseTypeEnum HospitalResponseType { get; set; }
    public virtual ICollection<SaveHospitalResponseBranchDto> HospitalResponseBranches { get; set; }
    public virtual ICollection<SaveHospitalResponseProcessDto> HospitalResponseProcesses { get; set; }
    public virtual ICollection<SaveHospitalResponseMaterialDto> HospitalResponseMaterials { get; set; }


}