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
using HTS.Enum;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    private readonly IConfiguration _config;
    public PaymentDocumentService(IRepository<PaymentDocument, int> paymentDocumentRepository,
        IRepository<Proforma, int> proformaRepository,
        IRepository<Payment, int> paymentRepository,
        IConfiguration config)
    {
        _paymentDocumentRepository = paymentDocumentRepository;
        _proformaRepository = proformaRepository;
        _paymentRepository = paymentRepository;
        _config = config;
    }
    
    public async Task<PaymentDocumentDto> GetAsync(int id)
    {
        var pd = await _paymentDocumentRepository.GetAsync(id);
        var fileBytes = File.ReadAllBytes($"{pd.FilePath}/{pd.FileName}");
        var paymentDocument = ObjectMapper.Map<PaymentDocument, PaymentDocumentDto>(pd);
        paymentDocument.File = Convert.ToBase64String(fileBytes);
        return paymentDocument;
    }
    
    public async Task SaveAsync(SavePaymentDocumentDto paymentDocument)
    {
        var entity = ObjectMapper.Map<SavePaymentDocumentDto, PaymentDocument>(paymentDocument);
        //Get entity from db
        var payment =
            (await _paymentRepository.WithDetailsAsync( (p => p.Proforma),
                (p => p.Proforma.Operation),
                (p => p.Proforma.Operation.PatientTreatmentProcess),
                (p => p.PaymentDocuments),
                (p => p.PaymentItems)))
            .FirstOrDefault(p => p.Id == paymentDocument.PaymentId);
        IsDataValidToSave(payment);
        entity.FilePath = string.Format(_config["FilePath:PaymentPath"], payment?.Proforma?.Operation?.PatientTreatmentProcess?.TreatmentCode,
            paymentDocument.FileName);
        SaveByteArrayToFileWithStaticMethod(paymentDocument.File, entity.FilePath);
        await _paymentDocumentRepository.DeleteManyAsync(payment.PaymentDocuments);
        await _paymentDocumentRepository.InsertAsync(entity);
        if (IsDataValidToFinalizePayment(payment))
        {
            payment.PaymentStatusId = EntityEnum.PaymentStatusEnum.PaymentCompleted.GetHashCode();
            payment.Proforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode();
            payment.Proforma.Operation.OperationStatusId =
                EntityEnum.OperationStatusEnum.PaymentCompletedTreatmentProcess.GetHashCode();
            payment.Proforma.Operation.PatientTreatmentProcess.TreatmentProcessStatusId =
                EntityEnum.PatientTreatmentStatusEnum.PaymentCompletedTreatmentProcess.GetHashCode();
            await _paymentRepository.UpdateAsync(payment);
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        await _paymentDocumentRepository.DeleteAsync(id);
        var payment =
            (await _paymentRepository.WithDetailsAsync( (p => p.Proforma),
                (p => p.Proforma.Operation),
                (p => p.Proforma.Operation.PatientTreatmentProcess)))
            .FirstOrDefault(p => p.PaymentDocuments.Any(d => d.Id == id));
        payment.PaymentStatusId = EntityEnum.PaymentStatusEnum.NewRecord.GetHashCode();
        payment.Proforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode();
        payment.Proforma.Operation.OperationStatusId =
            EntityEnum.OperationStatusEnum.ProformaApprovedWaitingForPayment.GetHashCode();
        payment.Proforma.Operation.PatientTreatmentProcess.TreatmentProcessStatusId =
            EntityEnum.PatientTreatmentStatusEnum.ProformaApprovedWaitingForPayment.GetHashCode();
        await _paymentRepository.UpdateAsync(payment);
    }
    
    private bool IsDataValidToFinalizePayment(Payment payment)
    {
        bool result = false;
        //If proforma amount and items amount equal mark as paied
        var paymentSum = payment.PaymentItems?.Sum(i => i.Price * i.ExchangeRate);
        if (paymentSum >= payment.Proforma.TotalProformaPrice)
        {
            result = true;
        }
        return result;
    }

    

    private void IsDataValidToSave(Payment payment)
    {
        if (payment == null)
        {
            throw new HTSBusinessException(ErrorCode.RelationalDataIsMissing);
        }
        /*if (payment.PaymentDocuments?.Any() ?? false)
        {
            throw new HTSBusinessException(ErrorCode.ThereCanOnlyBeOneDocument);
        }*/
    }
    
    private static void SaveByteArrayToFileWithStaticMethod(string data, string filePath)
    {
        FileInfo file = new System.IO.FileInfo(filePath);
        file.Directory?.Create(); // If the directory already exists, this method does nothing.
        File.WriteAllBytes(file.FullName, Convert.FromBase64String(data.Split(',')[1]));
    }
}