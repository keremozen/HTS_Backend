using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientDocument;
using HTS.Dto.PatientNote;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class PatientDocumentService : ApplicationService, IPatientDocumentService
{
    private readonly IRepository<PatientDocument, int> _patientDocumentRepository;
    private readonly IIdentityUserRepository _userRepository;
    public PatientDocumentService(IRepository<PatientDocument, int> nationalityRepository,
        IIdentityUserRepository userRepository)
    {
        _patientDocumentRepository = nationalityRepository;
        _userRepository = userRepository;
    }

    public async Task<PagedResultDto<PatientDocumentDto>> GetListAsync(int patientId)
    {
        //Get all entities
        var query = (await _patientDocumentRepository.WithDetailsAsync(p => p.Creator))
            .Where(p => p.PatientId == patientId);
        var responseList = ObjectMapper.Map<List<PatientDocument>, List<PatientDocumentDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _patientDocumentRepository.CountAsync();//item count
        return new PagedResultDto<PatientDocumentDto>(totalCount, responseList);
    }

    public async Task<PatientDocumentDto> CreateAsync(SavePatientDocumentDto patientNote)
    {
        var entity = ObjectMapper.Map<SavePatientDocumentDto, PatientDocument>(patientNote);
        entity.PatientDocumentStatusId = PatientDocumentStatusEnum.NewRecord.GetHashCode();
        await _patientDocumentRepository.InsertAsync(entity);
        return ObjectMapper.Map<PatientDocument, PatientDocumentDto>(entity);
    }

    public async Task<PatientDocumentDto> UpdateStatus(int id, int statusId)
    {
        var entity = await _patientDocumentRepository.GetAsync(id);
        entity.PatientDocumentStatusId = statusId;
        return ObjectMapper.Map<PatientDocument, PatientDocumentDto>(await _patientDocumentRepository.UpdateAsync(entity));
    }

    public async Task DeleteAsync(int id)
    {
        await _patientDocumentRepository.DeleteAsync(id);
    }


}