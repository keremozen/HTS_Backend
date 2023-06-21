using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HTS.Dto.Currency;
using HTS.Dto.Operation;
using HTS.Dto.ProformaAdditionalService;
using HTS.Dto.ProformaNotIncludingService;
using HTS.Dto.ProformaProcess;
using HTS.Enum;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace HTS.Dto.Proforma;

public class SaveProformaDto
{
    [Required]
    public int OperationId { get; set; }
    [Required]
    public int CurrencyId { get; set; }
    [Required]
    public EntityEnum.ProformaStatusEnum ProformaStatusId { get; set; }
    [Required]
    public decimal ExchangeRate { get; set; }
    [Required]
    public decimal TotalProformaPrice { get; set; }
    [Required]
    public string ProformaCode { get; set; }
    public string Description { get; set; }
    public string TPDescription { get; set; }
    public int Version { get; set; }

    public virtual ICollection<SaveProformaProcessDto> ProformaProcesses { get; set; }
    public virtual ICollection<SaveProformaAdditionalServiceDto> ProformaAdditionalServices { get; set; }
    public virtual ICollection<SaveProformaNotIncludingServiceDto> ProformaNotIncludingServices { get; set; }

}