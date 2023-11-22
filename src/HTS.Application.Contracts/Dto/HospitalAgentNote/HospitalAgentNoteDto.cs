using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.HospitalAgentNote
{
    public class HospitalAgentNoteDto : AuditedEntityWithUserDto<int,IdentityUserDto>
    {
        public string Note { get; set; }
        public int HospitalResponseId { get; set; }
        public HospitalAgentNoteStatusEnum StatusId { get; set; }
    }
}
