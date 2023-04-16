using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HTS.Dto.City
{
    public class SaveCityDto
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
