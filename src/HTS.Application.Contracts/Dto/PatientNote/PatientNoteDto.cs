using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PatientNote
{
    public class PatientNoteDto : AuditedEntityWithUserDto<int>
    {
        public string Note { get; set; }
        public int PatientId { get; set; }
    }
}
