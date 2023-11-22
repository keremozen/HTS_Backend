using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalAgentNote;
using HTS.Dto.PatientNote;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class HospitalAgentNoteService : ApplicationService, IHospitalAgentNoteService
{
    private readonly IRepository<HospitalAgentNote, int> _hospitalAgentNoteRepository;
    private readonly ICurrentUser _currentUser;
    public HospitalAgentNoteService(IRepository<HospitalAgentNote, int> hospitalAgentNoteRepository,
        ICurrentUser currentUser)
    {
        _hospitalAgentNoteRepository = hospitalAgentNoteRepository;
        _currentUser = currentUser;
    }
    [Authorize]
    public async Task<PagedResultDto<HospitalAgentNoteDto>> GetListAsync(int hospitalResponseId)
    {
        //Get all entities
        var query = (await _hospitalAgentNoteRepository.WithDetailsAsync(p => p.Creator))
            .Where(n => n.HospitalResponseId == hospitalResponseId);
        var responseList = ObjectMapper.Map<List<HospitalAgentNote>, List<HospitalAgentNoteDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = responseList.Count;//item count
        return new PagedResultDto<HospitalAgentNoteDto>(totalCount, responseList);
    }

    [Authorize("HTS.PatientManagement")]
    public async Task<HospitalAgentNoteDto> CreateAsync(SaveHospitalAgentNoteDto agentNote)
    {
        var entity = ObjectMapper.Map<SaveHospitalAgentNoteDto, HospitalAgentNote>(agentNote);
        entity.StatusId = HospitalAgentNoteStatusEnum.NewRecord.GetHashCode();
        await _hospitalAgentNoteRepository.InsertAsync(entity);
        return ObjectMapper.Map<HospitalAgentNote, HospitalAgentNoteDto>(entity);
    }

    [Authorize("HTS.PatientManagement")]
    public async Task<HospitalAgentNoteDto> UpdateStatus(int id, int statusId)
    {
        var entity = await _hospitalAgentNoteRepository.GetAsync(id);
        IsDataValidToUpdateStatus(entity, statusId);
        entity.StatusId = statusId;
        return ObjectMapper.Map<HospitalAgentNote, HospitalAgentNoteDto>(await _hospitalAgentNoteRepository.UpdateAsync(entity));
    }
    
    /// <summary>
    /// Check if everything is ok to change status
    /// </summary>
    /// <param name="agentNote">To be updated entity</param>
    /// <param name="statusId">New status</param>
    /// <exception cref="HTSBusinessException"></exception>
    private void IsDataValidToUpdateStatus(HospitalAgentNote agentNote, int statusId)
    {
        //Only created user can cancel note
        if (statusId == HospitalAgentNoteStatusEnum.Revoked.GetHashCode()
            &&  agentNote.CreatorId != _currentUser.Id)
        {
            throw new HTSBusinessException(ErrorCode.CreatorCanRevokeHospitalAgentNote);
        }
    }

    [Authorize("HTS.PatientManagement")]
    public async Task DeleteAsync(int id)
    {
        await _hospitalAgentNoteRepository.DeleteAsync(id);
    }
}