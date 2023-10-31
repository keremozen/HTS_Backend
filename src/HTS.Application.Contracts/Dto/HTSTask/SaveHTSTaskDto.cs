using System;
using HTS.Dto.Patient;
using HTS.Dto.TaskType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.HTSTask;

public class SaveHTSTaskDto
{
    public int? HospitalId { get; set; }
    public int PatientId { get; set; }
    public int TaskTypeId { get; set; }
    public int RelatedEntityId { get; set; }
}