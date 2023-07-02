using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.Proforma
{
    public class ProformaStatusDto : EntityDto<int>
    {
        public string Name { get; set; }
    }
}
