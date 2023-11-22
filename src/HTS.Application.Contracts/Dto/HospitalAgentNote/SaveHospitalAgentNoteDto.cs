using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HTS.Dto.HospitalAgentNote
{
    public class SaveHospitalAgentNoteDto
    {
        [Required]
        public string Note { get; set; }
        [Required]
        public int HospitalResponseId { get; set; }
    }
}
