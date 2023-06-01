using System;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.Hospital;
using HTS.Dto.OperationStatus;
using HTS.Dto.OperationType;
using HTS.Dto.TreatmentType;
using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.Operation;

public class OperationDto: EntityDto<int>
{
    public int HospitalResponseId { get; set; }
    public DateTime TravelDateToTurkey { get; set; }
    public DateTime TreatmentDate { get; set; }
    public int? TreatmentTypeId { get; set; }
    public bool? AnyInvitationLetter { get; set; }
    public int? PatientTreatmentProcessId { get; set; }
    public int? HospitalId { get; set; }
    public OperationTypeEnum OperationTypeId { get; set; }
    public OperationStatusEnum OperationStatusId { get; set; }
    public Guid? AppointedInterpreterId { get; set; }
    public TreatmentTypeDto TreatmentType { get; set; }
    public OperationTypeDto OperationType { get; set; }
    public OperationStatusDto OperationStatus { get; set; }
    public IdentityUserDto AppointedInterpreter { get; set; }
    public HospitalDto Hospital { get; set; }
}