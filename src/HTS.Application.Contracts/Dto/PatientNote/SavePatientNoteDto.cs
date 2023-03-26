using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HTS.Dto.PatientNote
{
    public class SavePatientNoteDto
    {
        [Required]
        public string Note { get; set; }
        public int PatientId { get; set; }
    }
}
