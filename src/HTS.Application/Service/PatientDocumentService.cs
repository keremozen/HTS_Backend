using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.PatientDocument;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;
public class PatientDocumentService : ApplicationService, IPatientDocumentService
{
    private readonly IRepository<PatientDocument, int> _patientDocumentRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IConfiguration _configuration;
    public PatientDocumentService(IRepository<PatientDocument, int> patientDocumentRepository,
        ICurrentUser currentUser,
        IConfiguration configuration)
    {
        _patientDocumentRepository = patientDocumentRepository;
        _currentUser = currentUser;
        _configuration = configuration;
    }

    [Authorize]
    public async Task<PatientDocumentDto> GetAsync(int id)
    {
       var pd = await _patientDocumentRepository.GetAsync(id);
       var fileBytes = File.ReadAllBytes($"{pd.FilePath}");
      var patientDocument = ObjectMapper.Map<PatientDocument, PatientDocumentDto>(pd);
      patientDocument.File = Convert.ToBase64String(fileBytes);
      return patientDocument;
    }

    [Authorize]

    public async Task<PagedResultDto<PatientDocumentDto>> GetListAsync(int patientId)
    {
        //Get all entities
        var query = (await _patientDocumentRepository.WithDetailsAsync((p => p.Creator),
                (p => p.DocumentType)))
            .Where(p => p.PatientId == patientId);
        var responseList = ObjectMapper.Map<List<PatientDocument>, List<PatientDocumentDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _patientDocumentRepository.CountAsync();//item count
        return new PagedResultDto<PatientDocumentDto>(totalCount, responseList);
    }

    [Authorize]
    public async Task<PagedResultDto<PatientDocumentDto>> GetDetailedListAsync(int patientId)
    {
        //Get all entities
        var query = (await _patientDocumentRepository.WithDetailsAsync((p => p.Creator),
                (p => p.DocumentType)))
            .Where(p => p.PatientId == patientId);
        var dbItems = await AsyncExecuter.ToListAsync(query);
        List<PatientDocumentDto> responseList = new List<PatientDocumentDto>();
        foreach (var patientDocument in dbItems)
        {
            var fileBytes = File.ReadAllBytes($"{patientDocument.FilePath}");
            var dto = ObjectMapper.Map<PatientDocument, PatientDocumentDto>(patientDocument);
            dto.File = Convert.ToBase64String(fileBytes);
            responseList.Add(dto);

        }
        var totalCount = await _patientDocumentRepository.CountAsync();//item count
        return new PagedResultDto<PatientDocumentDto>(totalCount, responseList);
    }

    [Authorize("HTS.PatientManagement")]
    public async Task<PatientDocumentDto> CreateAsync(SavePatientDocumentDto patientDocument)
    {
        var entity = ObjectMapper.Map<SavePatientDocumentDto, PatientDocument>(patientDocument);
        entity.PatientDocumentStatusId = PatientDocumentStatusEnum.NewRecord.GetHashCode();
        entity.FilePath = string.Format(_configuration["FilePath:PatientDocumentPath"], entity.PatientId,
            patientDocument.FileName);
        SaveByteArrayToFileWithStaticMethod(patientDocument.File, entity.FilePath);
        await _patientDocumentRepository.InsertAsync(entity);
        return ObjectMapper.Map<PatientDocument, PatientDocumentDto>(entity);
    }

    private static void SaveByteArrayToFileWithStaticMethod(string data, string filePath)
    {
        FileInfo file = new System.IO.FileInfo(filePath);
        file.Directory?.Create(); // If the directory already exists, this method does nothing.
        File.WriteAllBytes(file.FullName, Convert.FromBase64String(data.Split(',')[1]));
    }

    [Authorize("HTS.PatientManagement")]
    public async Task<PatientDocumentDto> UpdateStatus(int id, int statusId)
    {
        var entity = await _patientDocumentRepository.GetAsync(id);
        IsDataValidToUpdateStatus(entity, statusId);
        entity.PatientDocumentStatusId = statusId;
        return ObjectMapper.Map<PatientDocument, PatientDocumentDto>(await _patientDocumentRepository.UpdateAsync(entity));
    }

    [Authorize("HTS.PatientManagement")]
    public async Task DeleteAsync(int id)
    {
        await _patientDocumentRepository.DeleteAsync(id);
    }
    
    /// <summary>
    /// Check if everything is ok to change status
    /// </summary>
    /// <param name="patientDocument">To be updated entity</param>
    /// <param name="statusId">New status</param>
    /// <exception cref="HTSBusinessException"></exception>
    private void IsDataValidToUpdateStatus(PatientDocument patientDocument, int statusId)
    {
        //Only created user can cancel note
        if (statusId == PatientNoteStatusEnum.Revoked.GetHashCode()
            &&  patientDocument.CreatorId != _currentUser.Id)
        {
            throw new HTSBusinessException(ErrorCode.CreatorCanRevokePatientDocument);
        }
    }


}