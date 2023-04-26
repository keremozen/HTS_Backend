using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientNote;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class PatientNoteService : ApplicationService, IPatientNoteService
{
    private readonly IRepository<PatientNote, int> _patientNoteRepository;
    private readonly IIdentityUserRepository _userRepository;
    public PatientNoteService(IRepository<PatientNote, int> nationalityRepository,
        IIdentityUserRepository userRepository)
    {
        _patientNoteRepository = nationalityRepository;
        _userRepository = userRepository;
    }

    public async Task<PagedResultDto<PatientNoteDto>> GetListAsync(int patientId)
    {
        //Get all entities
        var query = (await _patientNoteRepository.WithDetailsAsync(p => p.Creator))
            .Where(p => p.PatientId == patientId);
        var responseList = ObjectMapper.Map<List<PatientNote>, List<PatientNoteDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _patientNoteRepository.CountAsync();//item count
        return new PagedResultDto<PatientNoteDto>(totalCount, responseList);
    }

    public async Task<PatientNoteDto> CreateAsync(SavePatientNoteDto patientNote)
    {
        var entity = ObjectMapper.Map<SavePatientNoteDto, PatientNote>(patientNote);
        entity.PatientNoteStatusId = PatientNoteStatusEnum.NewRecord.GetHashCode();
        await _patientNoteRepository.InsertAsync(entity);
        return ObjectMapper.Map<PatientNote, PatientNoteDto>(entity);
    }

    public async Task<PatientNoteDto> UpdateStatus(int id, int statusId)
    {
        var entity = await _patientNoteRepository.GetAsync(id);
        entity.PatientNoteStatusId = statusId;
        return ObjectMapper.Map<PatientNote, PatientNoteDto>(await _patientNoteRepository.UpdateAsync(entity));
    }

    public async Task DeleteAsync(int id)
    {
        await _patientNoteRepository.DeleteAsync(id);
    }
}