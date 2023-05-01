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
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class HospitalConsultationDocumentService : ApplicationService,IHospitalConsultationDocumentService
{
    private readonly IRepository<PatientDocument, int> _patientDocumentRepository;
    
    public HospitalConsultationDocumentService(IRepository<PatientDocument, int> patientDocumentRepository)
    {
        _patientDocumentRepository = patientDocumentRepository;
    }
    

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