using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.HospitalizationType;
using HTS.Dto.HospitalResponseBranch;
using HTS.Dto.HospitalResponseMaterial;
using HTS.Dto.HospitalResponseProcess;
using HTS.Dto.HospitalResponseType;
using Volo.Abp.Application.Dtos;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.HospitalResponse;

public class HospitalResponseDto : EntityDto<int>
{
    public int HospitalConsultationId { get; set; }
    public string Description { get; set; }
    public HospitalResponseTypeEnum HospitalResponseTypeId { get; set; }
    public HospitalizationTypeEnum? HospitalizationTypeId { get; set; }
    public DateTime PossibleTreatmentDate { get; set; }
    public int? HospitalizationNumber { get; set; }
     public bool IsManuallyAdded { get; set; }
    
    public HospitalResponseTypeDto HospitalResponseType { get; set; }
    public HospitalizationTypeDto HospitalizationType { get; set; }
    public ICollection<HospitalResponseBranchDto> HospitalResponseBranches { get; set; }
    public ICollection<HospitalResponseProcessDto> HospitalResponseProcesses { get; set; }
    public ICollection<HospitalResponseMaterialDto> HospitalResponseMaterials { get; set; }
}