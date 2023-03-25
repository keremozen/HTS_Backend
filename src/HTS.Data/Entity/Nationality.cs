﻿using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace HTS.Data.Entity
{
    public class Nationality : FullAuditedEntity<int>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(10)]
        public string PhoneCode { get; set; }
        public bool IsActive { get; set; }
    }
}
