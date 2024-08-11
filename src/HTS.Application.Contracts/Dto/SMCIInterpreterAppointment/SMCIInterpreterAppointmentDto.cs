using System;
using HTS.Dto.Branch;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.SMCIInterpreterAppointment;

public class SMCIInterpreterAppointmentDto: EntityDto<int>
{
    public int SalesMethodAndCompanionInfoId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }
    public int? BranchId { get; set; }
    public BranchDto Branch { get; set; }
}