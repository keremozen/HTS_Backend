using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.SalesMethodAndCompanionInfo;

public class SalesMethodAndCompanionInfoDto :EntityDto<int>
{
    public string CompanionNameSurname { get; set; }
    public string CompanionEmail { get; set; }
    public string CompanionPhoneNumber { get; set; }
    public string CompanionRelationship { get; set; }
    public string CompanionPassportNumber { get; set; }
    public int PatientTreatmentProcessId { get; set; }
    public int? PatientAdmissionMethodId { get; set; }
    public int? ContractedInstitutionId { get; set; }
    public int? ContractedInstitutionStaffId { get; set; }
    public int? PhoneCountryCodeId { get; set; }
    public int? CompanionNationalityId { get; set; } 
}