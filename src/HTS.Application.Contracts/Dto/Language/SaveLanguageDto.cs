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

        [Required, StringLength(10)]
        public string Code { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
