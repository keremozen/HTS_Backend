using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HTS.Dto.PatientAdmissionMethod
{
    public class SavePatientAdmissionMethodDto
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
