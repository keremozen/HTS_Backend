using System;
using System.ComponentModel.DataAnnotations;
using HTS.Dto.Branch;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.SMCIInterpreterAppointment;

public class SaveSMCIInterpreterAppointmentDto: EntityDto<int>
{
    [Required]
    public int SalesMethodAndCompanionInfoId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }
    public int? BranchId { get; set; }
}