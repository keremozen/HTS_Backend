using System;
using HTS.Dto.Patient;
using HTS.Dto.TaskType;
using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.HTSTask;

public class SaveHTSTaskDto
{
    public int? HospitalId { get; set; }
    public int PatientId { get; set; }
    public EntityEnum.TaskTypeEnum TaskType { get; set; }
    public int RelatedEntityId { get; set; }
    public string TreatmentCode { get; set; }
}