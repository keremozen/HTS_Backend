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
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;


public class HospitalConsultationDocumentService : ApplicationService, IHospitalConsultationDocumentService
{
    private readonly IRepository<PatientDocument, int> _patientDocumentRepository;
    private readonly IRepository<HospitalConsultationDocument, int> _hospitalConsultationDocumentRepository;

    public HospitalConsultationDocumentService(
        IRepository<PatientDocument, int> patientDocumentRepository,
        IRepository<HospitalConsultationDocument, int> hospitalConsultationDocumentRepository)
    {
        _patientDocumentRepository = patientDocumentRepository;
        _hospitalConsultationDocumentRepository = hospitalConsultationDocumentRepository;
    }


    public async Task<HospitalConsultationDocumentDto> GetAsync(int id)
    {
        var hcd = await _hospitalConsultationDocumentRepository.GetAsync(id);
        var fileBytes = File.ReadAllBytes($"{hcd.FilePath}");
        var document = ObjectMapper.Map<HospitalConsultationDocument, HospitalConsultationDocumentDto>(hcd);
        document.File = Convert.ToBase64String(fileBytes);
        return document;
    }

    [Authorize]
    public async Task<PagedResultDto<HospitalConsultationDocumentDto>> ForwardDocumentsAsync(int patientId)
    {
        var query = await _patientDocumentRepository.GetQueryableAsync();
        query = query.Where(pd => pd.PatientId == patientId
        && pd.PatientDocumentStatusId != PatientDocumentStatusEnum.Revoked.GetHashCode());

        var responseList = ObjectMapper.Map<List<PatientDocument>, List<HospitalConsultationDocumentDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _patientDocumentRepository.CountAsync();//item count

        return new PagedResultDto<HospitalConsultationDocumentDto>(totalCount, responseList);
    }



}