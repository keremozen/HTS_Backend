using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTS.Dto.SalesMethodAndCompanionInfo;

public class SaveSalesMethodAndCompanionInfoDto
{
    [StringLength(500)]
    public string CompanionNameSurname { get; set; }
    [StringLength(50)]
    public string CompanionEmail { get; set; }
    [StringLength(20)]
    public string CompanionPhoneNumber { get; set; }
    [StringLength(20)]
    public string CompanionRelationship { get; set; }
    [StringLength(50)]
    public string CompanionPassportNumber { get; set; }
        
    public int PatientTreatmentProcessId { get; set; }
    public int? PatientAdmissionMethodId { get; set; }
    public int? ContractedInstitutionId { get; set; }
    public int? ContractedInstitutionStaffId { get; set; }
    public int? PhoneCountryCodeId { get; set; }
    public int? CompanionNationalityId { get; set; }
    
    public Guid? AppointedInterpreterId { get; set; }
    public bool AnyInvitationLetter { get; set; }
    public bool IsDocumentTranslationRequired { get; set; }
    public bool AdvancePaymentRequested { get; set; }
    public bool AnyTravelPlan { get; set; }
    public DateTime? TravelDateToTurkey { get; set; }
    [StringLength(500)]
    public string TurkeyDestination { get; set; }
    public string TravelDescription { get; set; }
    public DateTime? TreatmentDate { get; set; }
}