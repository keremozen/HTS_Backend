using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
namespace HTS.Data.Entity
{

    public class SalesMethodAndCompanionInfo : AuditedEntity<int>
    {
        [StringLength(500)]
        public string CompanionNameSurname { get; set; }
        [StringLength(50)]
        public string CompanionEmail { get; set; }

        [StringLength(20)]
        public string CompanionPhoneNumber { get; set; }

        [StringLength(20)]
        public string CompanionRelationship { get; set; }


        public PatientTreatmentProcess PatientTreatmentProcess { get; set; }
        public PatientAdmissionMethod PatientAdmissionMethod { get; set; }
        public ContractedInstitution ContractedInstitution { get; set; }
        public ContractedInstitutionStaff ContractedInstitutionStaff { get; set; }
        public Nationality PhoneCountryCode { get; set; }
        public Nationality CompanionNationality { get; set; }



    }
}
