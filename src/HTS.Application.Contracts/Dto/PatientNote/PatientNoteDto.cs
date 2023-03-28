using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.PatientNote
{
    public class PatientNoteDto : AuditedEntityWithUserDto<int,IdentityUserDto>
    {
        public string Note { get; set; }
        public int PatientId { get; set; }
        public PatientNoteStatusEnum PatientNoteStatusId { get; set; }
    }
}
