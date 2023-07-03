using System;
using System.Collections.Generic;
using HTS.Dto.Gender;
using HTS.Dto.Hospital;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientTreatmentProcess;
using HTS.Dto.PaymentItem;
using HTS.Dto.PaymentReason;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.Payment;

public class PaymentDto: AuditedEntityWithUserDto<int, IdentityUserDto>
{
    public int? ProformaId { get; set; }
    public int PtpId { get; set; }
    public int HospitalId { get; set; }
    public string RowNumber { get; set; }
    public string PatientNameSurname { get; set; }
    public string PaidNameSurname { get; set; }
    public string ProcessingUserNameSurname { get; set; }
    public int PaymentReasonId { get; set; }
    public string ProcessingNumber { get; set; }
    public string FileNumber { get; set; }
    public string Description { get; set; }
    public DateTime PaymentDate { get; set; }

    public HospitalDto Hospital { get; set; }
    public PaymentReasonDto PaymentReason { get; set; }
        
    public List<PaymentItemDto> PaymentItems { get; set; }
}
