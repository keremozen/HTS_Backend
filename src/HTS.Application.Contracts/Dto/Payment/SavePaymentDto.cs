using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.PaymentItem;

namespace HTS.Dto.Payment;

public class SavePaymentDto
{
    [Required]
    public int? ProformaId { get; set; }
    [Required]
    public int PtpId { get; set; }
    [Required]
    public int HospitalId { get; set; }
    [Required]
    public string PayerNameSurname { get; set; }
    [Required]
    public int PaymentReasonId { get; set; }
    public string ProcessingNumber { get; set; }
    public string FileNumber { get; set; }
    public string Description { get; set; }
    
    public List<SavePaymentItemDto> PaymentItems { get; set; }
}