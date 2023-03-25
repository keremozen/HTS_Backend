using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PatientNoteStatus
{
    public class PatientNoteStatusDto : EntityDto<int>
    {
        public string Name { get; set; }
    }
}
