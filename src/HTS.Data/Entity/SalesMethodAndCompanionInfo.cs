﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class SalesMethodAndCompanionInfo : AuditedEntityWithUser<int, IdentityUser>
    {
        [StringLength(500)]
        public string? CompanionNameSurname { get; set; }
        [StringLength(50)]
        public string? CompanionEmail { get; set; }
        [StringLength(20)]
        public string? CompanionPhoneNumber { get; set; }
        [StringLength(20)]
        public string? CompanionRelationship { get; set; }
        [StringLength(50)]
        public string? CompanionPassportNumber { get; set; }
        
        public int PatientTreatmentProcessId { get; set; }
        public int? PatientAdmissionMethodId { get; set; }
        public int? ContractedInstitutionId { get; set; }
        public int? ContractedInstitutionStaffId { get; set; }
        public int? PhoneCountryCodeId { get; set; }
        public int? CompanionNationalityId { get; set; }

        [ForeignKey("PatientTreatmentProcessId")]
        public PatientTreatmentProcess PatientTreatmentProcess { get; set; }
        [ForeignKey("PatientAdmissionMethodId")]
        public PatientAdmissionMethod? PatientAdmissionMethod { get; set; }
        [ForeignKey("ContractedInstitutionId")]
        public ContractedInstitution? ContractedInstitution { get; set; }
        [ForeignKey("ContractedInstitutionStaffId")]
        public ContractedInstitutionStaff? ContractedInstitutionStaff { get; set; }
        [ForeignKey("PhoneCountryCodeId")]
        public Nationality? PhoneCountryCode { get; set; }
        [ForeignKey("CompanionNationalityId")]
        public Nationality? CompanionNationality { get; set; }
        public virtual ICollection<InvitationLetterDocument> InvitationLetterDocuments { get; set; }
    }
}
