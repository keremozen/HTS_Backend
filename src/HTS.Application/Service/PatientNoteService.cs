using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.PatientNote;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;
[Authorize("HTS.PatientManagement")]
public class PatientNoteService : ApplicationService, IPatientNoteService
{
    private readonly IRepository<PatientNote, int> _patientNoteRepository;
    private readonly ICurrentUser _currentUser;
    public PatientNoteService(IRepository<PatientNote, int> nationalityRepository,
        ICurrentUser currentUser)
    {
        _patientNoteRepository = nationalityRepository;
        _currentUser = currentUser;
    }

    public async Task<PagedResultDto<PatientNoteDto>> GetListAsync(int patientId)
    {
        //Get all entities
        var query = (await _patientNoteRepository.WithDetailsAsync(p => p.Creator))
            .Where(p => p.PatientId == patientId);
        var responseList = ObjectMapper.Map<List<PatientNote>, List<PatientNoteDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = responseList.Count;//item count
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
        IsDataValidToUpdateStatus(entity, statusId);
        entity.PatientNoteStatusId = statusId;
        return ObjectMapper.Map<PatientNote, PatientNoteDto>(await _patientNoteRepository.UpdateAsync(entity));
    }
    
    /// <summary>
    /// Check if everything is ok to change status
    /// </summary>
    /// <param name="patientNote">To be updated entity</param>
    /// <param name="statusId">New status</param>
    /// <exception cref="HTSBusinessException"></exception>
    private void IsDataValidToUpdateStatus(PatientNote patientNote, int statusId)
    {
        //Only created user can cancel note
        if (statusId == PatientNoteStatusEnum.Revoked.GetHashCode()
            &&  patientNote.CreatorId != _currentUser.Id)
        {
            throw new HTSBusinessException(ErrorCode.CreatorCanRevokePatientNote);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _patientNoteRepository.DeleteAsync(id);
    }
}