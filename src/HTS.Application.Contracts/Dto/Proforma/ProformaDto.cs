using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.Currency;
using HTS.Dto.Operation;
using HTS.Dto.ProformaAdditionalService;
using HTS.Dto.ProformaNotIncludingService;
using HTS.Dto.ProformaProcess;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.Proforma;

public class ProformaDto  : AuditedEntityWithUserDto<int,IdentityUserDto>
{
    public int OperationId { get; set; }
    public int CurrencyId { get; set; }
    public decimal CurrencyAmount { get; set; }
    public DateTime CreationDate { get; set; }
    public string Description { get; set; }
    public string TPDescription { get; set; }
    public int Version { get; set; }
    public decimal TotalProformaAmount { get; set; }
        
    public CurrencyDto Currency { get; set; }
    public OperationDto Operation { get; set; }
    public virtual ICollection<ProformaProcessDto> ProformaProcesses { get; set; }
    public virtual ICollection<ProformaAdditionalServiceDto> ProformaAdditionalServices { get; set; }
    public virtual ICollection<ProformaNotIncludingServiceDto> ProformaNotIncludingServices { get; set; }
    
}