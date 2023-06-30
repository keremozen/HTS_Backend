using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.Currency;
using HTS.Dto.Operation;
using HTS.Dto.ProformaAdditionalService;
using HTS.Dto.ProformaNotIncludingService;
using HTS.Dto.ProformaProcess;
using HTS.Dto.RejectReason;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Dto.Proforma;

public class ProformaPricingListDto : AuditedEntityWithUserDto<int, IdentityUserDto>
{
    public int OperationId { get; set; }
    public ProformaStatusEnum ProformaStatusId { get; set; }
    public string ProformaCode { get; set; }
    public DateTime CreationDate { get; set; }
    public int Version { get; set; }
    public int? RejectReasonId { get; set; }
    public string RejectReasonMFB { get; set; }
    public RejectReasonDto RejectReason { get; set; }
}