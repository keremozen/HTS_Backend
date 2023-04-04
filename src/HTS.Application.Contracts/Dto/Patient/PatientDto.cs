using HTS.Dto.Gender;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.Patient
{
    public class PatientDto : AuditedEntityWithUserDto<int, IdentityUserDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? PhoneCountryCodeId { get; set; }
        public int NationalityId { get; set; }
        public int? GenderId { get; set; }
        public int? MotherTongueId { get; set; }
        public int? SecondTongueId { get; set; }
        public GenderDto Gender { get; set; }
        public LanguageDto SecondTongue { get; set; }
        public LanguageDto MotherTongue { get; set; }
    }
}
