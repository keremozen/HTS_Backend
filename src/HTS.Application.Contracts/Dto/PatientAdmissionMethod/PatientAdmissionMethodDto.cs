﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.PatientAdmissionMethod
{
    public class PatientAdmissionMethodDto : AuditedEntityWithUserDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}