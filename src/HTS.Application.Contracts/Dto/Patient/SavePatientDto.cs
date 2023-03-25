using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HTS.Dto.Patient
{
    public class SavePatientDto
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Surname { get; set; }

        [StringLength(500)]
        public string PassportNumber { get; set; }

        public DateTime? BirthDate { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Email { get; set; }

        public int? PhoneCountryCodeId { get; set; }
        public int NationalityId { get; set; }
        public int? GenderId { get; set; }
        public int? MotherTongueId { get; set; }
        public int? SecondTongueId { get; set; }

    }
}
