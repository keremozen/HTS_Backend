using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HTS.Dto.Language
{
    public class SaveLanguageDto
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
