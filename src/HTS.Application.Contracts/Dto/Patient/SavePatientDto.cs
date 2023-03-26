using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HTS.Dto.Patient
{
    public class SavePatientDto : IValidatableObject
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Surname { get; set; }

        [StringLength(500)]
        public string PassportNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public int? PhoneCountryCodeId { get; set; }
        public int NationalityId { get; set; }
        public int? GenderId { get; set; }
        public int? MotherTongueId { get; set; }
        public int? SecondTongueId { get; set; }

        public IEnumerable<ValidationResult> Validate(
           ValidationContext validationContext)
        {
            if ((PhoneCountryCodeId != null && string.IsNullOrEmpty(PhoneNumber)) ||
                PhoneCountryCodeId == null && !string.IsNullOrEmpty(PhoneNumber))
            {
                yield return new ValidationResult(
                    "Phone country code and phone number should be filled to gether.",
                    new[] { "Phone country code", "Phone number" }
                );
            }
        }

    }
}
