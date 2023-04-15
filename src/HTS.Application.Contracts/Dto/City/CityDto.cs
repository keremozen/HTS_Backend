using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.City
{
    public class CityDto : EntityDto<int>
    {
        public string Name { get; set; }
    }
}
