using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.PatientNote
{
    public class PatientNoteDto : AuditedEntityWithUserDto<int,IdentityUserDto>
    {
        public string Note { get; set; }
        public int PatientId { get; set; }
    }
}
