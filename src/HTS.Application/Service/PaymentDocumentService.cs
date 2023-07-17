using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalConsultationDocument;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

[Authorize]
public class PaymentDocumentService : ApplicationService//,IPaymentDocumentService
{
    private readonly IRepository<PaymentDocument, int> _paymentDocumentRepository;
    
    public PaymentDocumentService(IRepository<PaymentDocument, int> paymentDocumentRepository)
    {
        _paymentDocumentRepository = paymentDocumentRepository;
    }
    
}