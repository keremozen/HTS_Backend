using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.HospitalAgentNote
{
    public class SaveHospitalAgentNoteDto
    {
        [Required]
        public string Note { get; set; }
        [Required]
        public int HospitalResponseId { get; set; }
        public HospitalAgentNoteStatusEnum StatusId { get; set; }
    }
}
