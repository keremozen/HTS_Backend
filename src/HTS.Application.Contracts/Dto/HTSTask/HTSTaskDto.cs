using System;
using HTS.Dto.Patient;
using HTS.Dto.TaskType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.HTSTask;

public class HTSTaskDto: EntityDto<int>
{
    public Guid UserId { get; set; }
    public int PatientId { get; set; }
    public int TaskTypeId { get; set; }
    public bool IsActive { get; set; }
    public string Url { get; set; }
    public int RelatedEntityId { get; set; }
    public TaskTypeDto TaskType { get; set; }
    public PatientDto Patient { get; set; }
    public IdentityUserDto User { get; set; }
}