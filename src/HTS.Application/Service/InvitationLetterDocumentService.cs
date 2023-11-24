using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Common;
using HTS.Data.Entity;
using HTS.Dto.InvitationLetterDocument;
using HTS.Dto.PatientDocument;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;
public class InvitationLetterDocumentService : ApplicationService, IInvitationLetterDocumentService
{
    private readonly IRepository<InvitationLetterDocument, int> _documentRepository;
    private readonly IRepository<SalesMethodAndCompanionInfo, int> _salesMethodAndCompanionInfoRepository;
    private readonly IRepository<Proforma, int> _proformaRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IConfiguration _configuration;
    public InvitationLetterDocumentService(IRepository<InvitationLetterDocument, int> documentRepository,
        IRepository<SalesMethodAndCompanionInfo, int> salesMethodAndCompanionInfoRepository,
        IRepository<Proforma, int> proformaRepository,
        ICurrentUser currentUser,
        IConfiguration configuration)
    {
        _documentRepository = documentRepository;
        _salesMethodAndCompanionInfoRepository = salesMethodAndCompanionInfoRepository;
        _proformaRepository = proformaRepository;
        _currentUser = currentUser;
        _configuration = configuration;
    }

   
    
    public async Task UploadAsync(SaveDocumentDto document)
    {
       var salesMethodEntity= await (await _salesMethodAndCompanionInfoRepository.WithDetailsAsync(s => s.PatientTreatmentProcess))
           .AsNoTracking()
           .FirstOrDefaultAsync(s => s.Id == document.SalesMethodAndCompanionInfoId);

        var entity = ObjectMapper.Map<SaveDocumentDto, InvitationLetterDocument>(document);
        entity.FilePath = string.Format(_configuration["FilePath:HospitalConsultationPath"], salesMethodEntity.PatientTreatmentProcess.PatientId,
            document.FileName);
        SaveByteArrayToFileWithStaticMethod(document.File, entity.FilePath);
        await _documentRepository.InsertAsync(entity);
    }
    
    

    public async Task SendEMailToPatient(int salesMethodId)
    {
        var salesMethodEntity= await (await _salesMethodAndCompanionInfoRepository.WithDetailsAsync((s => s.PatientTreatmentProcess),
                (s => s.PatientTreatmentProcess.Patient),
                (s => s.InvitationLetterDocuments)))
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == salesMethodId);

        if (salesMethodEntity != null)
        {
            var proforma = await (await _proformaRepository.GetQueryableAsync())
                .Include(p => p.Operation)
                .ThenInclude(o => o.Hospital)
                .FirstOrDefaultAsync(p =>
                p.Operation.PatientTreatmentProcessId == salesMethodEntity.PatientTreatmentProcessId
                && (p.ProformaStatusId == ProformaStatusEnum.PaymentCompleted.GetHashCode() ||
                    p.ProformaStatusId == ProformaStatusEnum.WaitingForPayment.GetHashCode()));
            if (proforma == null || proforma.Operation.HospitalId == null)
            {
                throw new HTSBusinessException(ErrorCode.ThereIsNoHospitalOrApprovedProforma);
            }

            //Send mail
            string mailBody =
                $"Dear {salesMethodEntity.PatientTreatmentProcess.Patient.Name} {salesMethodEntity.PatientTreatmentProcess.Patient.Surname}," +
                $"</br></br>First of all, thank you for choosing us. We invite you to {proforma.Operation.Hospital.Name}. " +
                $"Hospital based on your treatment plan that we have given in the appendix. If you submit the documents we have requested from you, your treatment process will be initiated.</br>" +
                $"Thanks.</br>We wish you a nice day.</br>";
            var fileBytes = File.ReadAllBytes($"{salesMethodEntity.InvitationLetterDocuments.FirstOrDefault()?.FilePath}");
            var mailSubject = "Invitation Letter";
            Helper.SendMail(salesMethodEntity.PatientTreatmentProcess.Patient.Email,
                mailBody, fileBytes, subject: mailSubject);
        }
    }
    private static void SaveByteArrayToFileWithStaticMethod(string data, string filePath)
    {
        FileInfo file = new System.IO.FileInfo(filePath);
        file.Directory?.Create(); // If the directory already exists, this method does nothing.
        File.WriteAllBytes(file.FullName, Convert.FromBase64String(data.Split(',')[1]));
    }
    
    
}