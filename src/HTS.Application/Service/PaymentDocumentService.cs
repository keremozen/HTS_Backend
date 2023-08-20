using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalConsultationDocument;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Dto.PatientDocument;
using HTS.Dto.PaymentDocument;
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
public class PaymentDocumentService : ApplicationService,IPaymentDocumentService
{
    private readonly IRepository<PaymentDocument, int> _paymentDocumentRepository;
    private readonly IRepository<Payment, int> _paymentRepository;
    private readonly IRepository<Proforma, int> _proformaRepository;
    public PaymentDocumentService(IRepository<PaymentDocument, int> paymentDocumentRepository,
        IRepository<Proforma, int> proformaRepository,
        IRepository<Payment, int> paymentRepository)
    {
        _paymentDocumentRepository = paymentDocumentRepository;
        _proformaRepository = proformaRepository;
        _paymentRepository = paymentRepository;
    }
    
    public async Task SaveAsync(SavePaymentDocumentDto paymentDocument)
    {
        var entity = ObjectMapper.Map<SavePaymentDocumentDto, PaymentDocument>(paymentDocument);
        //Get entity from db
        var payment =
            (await _paymentRepository.WithDetailsAsync( (p => p.Proforma),
                (p => p.Proforma.Operation),
                (p => p.Proforma.Operation.PatientTreatmentProcess)))
            .FirstOrDefault(p => p.Id == paymentDocument.PaymentId); 
        IsDataValidToSave(payment);
        entity.FilePath = @"\\10.72.17.12\filesrv\"+payment?.Proforma?.Operation?.PatientTreatmentProcess?.TreatmentCode;
        SaveByteArrayToFileWithStaticMethod(paymentDocument.File, entity.FilePath);
        await _paymentDocumentRepository.InsertAsync(entity);
        
    }
    
    public async Task DeleteAsync(int id)
    {
        //TODO:Hopsy entity type may be changed - Hard delete or soft delete
        await _paymentDocumentRepository.DeleteAsync(id);
    }
    

    private void IsDataValidToSave(Payment payment)
    {
        if (payment == null)
        {
            throw new HTSBusinessException(ErrorCode.RelationalDataIsMissing);
        }
    }
    
    private static void SaveByteArrayToFileWithStaticMethod(string data, string filePath)
    {
        FileInfo file = new System.IO.FileInfo(filePath);
        file.Directory?.Create(); // If the directory already exists, this method does nothing.
        File.WriteAllBytes(file.FullName, Convert.FromBase64String(data.Split(',')[1]));
    }
}