﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class Process : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string? Description { get; set; }
        public int ProcessTypeId { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("ProcessTypeId")]
        public ProcessType ProcessType { get; set; }
     
    }
}