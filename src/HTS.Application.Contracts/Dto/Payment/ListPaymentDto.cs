using System;
using System.Collections.Generic;
using HTS.Dto.Gender;
using HTS.Dto.Hospital;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientTreatmentProcess;
using HTS.Dto.PaymentItem;
using HTS.Dto.PaymentReason;
using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.Payment;

public class ListPaymentDto: EntityDto<int>
{
    public int ProformaId { get; set; }
    public int PtpId { get; set; }
    public int HospitalId { get; set; }
    public int RowNumber { get; set; }
    public string GeneratedRowNumber { get; set; }
    public string PatientNameSurname { get; set; }
    public string PayerNameSurname { get; set; }
    public string CollectorNameSurname { get; set; }
    public int PaymentReasonId { get; set; }
    public string ProcessingNumber { get; set; }
    public string FileNumber { get; set; }
    public string ProformaNumber { get; set; }
    public string Description { get; set; }
    public DateTime PaymentDate { get; set; }
    public EntityEnum.PaymentStatusEnum PaymentStatusId { get; set; }
    public decimal TotalPrice { get; set; }
    public PaymentReasonDto PaymentReason { get; set; }
}
