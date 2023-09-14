using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Common;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Interface;
using HTS.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class HospitalConsultationService : ApplicationService, IHospitalConsultationService
{
    private readonly IRepository<HospitalConsultation, int> _hcRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _ptpRepository;
    private readonly IRepository<Hospital, int> _hospitalRepository;
    private readonly IStringLocalizer<HTSResource> _localizer;
    private IConfiguration _config;

    public HospitalConsultationService(IRepository<HospitalConsultation, int> hcRepository,
        IRepository<PatientTreatmentProcess, int> ptpRepository,
        IRepository<Hospital, int> hospitalRepository,
        IStringLocalizer<HTSResource> localizer,
        IConfiguration config)
    {
        _hcRepository = hcRepository;
        _ptpRepository = ptpRepository;
        _hospitalRepository = hospitalRepository;
        _localizer = localizer;
        _config = config;
    }

    public async Task<HospitalConsultationDto> GetAsync(int id)
    {
        var query = (await _hcRepository.WithDetailsAsync()).Where(p => p.Id == id);
        var consultation = await AsyncExecuter.FirstOrDefaultAsync(query);
        return ObjectMapper.Map<HospitalConsultation, HospitalConsultationDto>(consultation);
    }

    [Authorize("HTS.PatientList")]
    public async Task<PagedResultDto<HospitalConsultationDto>> GetByPatientTreatmenProcessAsync(int ptpId)
    {
        var query = await _hcRepository.WithDetailsAsync();
        query = query.Where(hc => hc.PatientTreatmentProcessId == ptpId);

        var responseList = ObjectMapper.Map<List<HospitalConsultation>, List<HospitalConsultationDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _hcRepository.CountAsync();//item count

        return new PagedResultDto<HospitalConsultationDto>(totalCount, responseList);
    }

    [Authorize("HTS.HospitalConsultation")]
    public async Task CreateAsync(SaveHospitalConsultationDto hospitalConsultation)
    {
        await IsDataValidToSave(hospitalConsultation);
        int rowNumber = await GetRowNumber(hospitalConsultation.PatientTreatmentProcessId);
        List<HospitalConsultation> entityList = new List<HospitalConsultation>();
        HospitalConsultation entity;
        foreach (var hospital in hospitalConsultation.HospitalIds)
        {
            entity = ObjectMapper.Map<SaveHospitalConsultationDto, HospitalConsultation>(hospitalConsultation);
            entity.RowNumber = rowNumber;
            entity.HospitalId = hospital;
            entity.HospitalConsultationStatusId = HospitalConsultationStatusEnum.HospitalResponseWaiting.GetHashCode();
            ProcessHospitalConsultationDocuments(entity, hospitalConsultation);
            entityList.Add(entity);
        }

        var ptpEntity = (await _ptpRepository.GetQueryableAsync()).FirstOrDefault(ptp => ptp.Id == hospitalConsultation.PatientTreatmentProcessId);
        if (ptpEntity != null)
        {
            ptpEntity.TreatmentProcessStatusId = PatientTreatmentStatusEnum.HospitalAskedWaitingResponse.GetHashCode();
            await _ptpRepository.UpdateAsync(ptpEntity);
        }


        await _hcRepository.InsertManyAsync(entityList, true);
        await SendEMailToHospitalUHBs(entityList);
    }

    private async Task SendEMailToHospitalUHBs(List<HospitalConsultation> entityList)
    {
        //Send mail to hospital consultations
        string mailBodyFormat = _localizer["HospitalConsultation:MailBody"];
        string urlFormat = _config["ServiceURL:HospitalResponseURLFormat"];
        var hospitalIds = entityList.Select(c => c.HospitalId).ToList();
        var hospitals = await (await _hospitalRepository.WithDetailsAsync(h => h.HospitalUHBStaffs))
            .Where(h => hospitalIds.Contains(h.Id)).ToListAsync();
        foreach (var hc in entityList)
        {
            var hospital = hospitals.FirstOrDefault(h => h.Id == hc.HospitalId);
            var uhbList = hospital?.HospitalUHBStaffs.Select(s => s.Email).ToList();
            if (uhbList?.Any() ?? false)
            {
                #if !DEBUG
                Helper.SendMail(uhbList, string.Format(mailBodyFormat, string.Format(urlFormat, RandomString(8), Convert.ToBase64String(Encoding.UTF8.GetBytes(hc.Id.ToString())))));
                #endif
            }
        }
    }

    private string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private async Task<int> GetRowNumber(int ptProcessId)
    {
        var query = await _hcRepository.GetQueryableAsync();
        int rowNumber = query.Where(hc => hc.PatientTreatmentProcessId == ptProcessId).Max(hc => (int?)hc.RowNumber) ?? 0;
        return ++rowNumber;
    }

    private void ProcessHospitalConsultationDocuments(HospitalConsultation entity, SaveHospitalConsultationDto hospitalConsultation)
    {
        foreach (var document in entity.HospitalConsultationDocuments)
        {
            var userDocument = hospitalConsultation.HospitalConsultationDocuments.FirstOrDefault(d => d.FileName == document.FileName);
            document.FilePath = string.Format(_config["FilePath:HospitalConsultationPath"], entity.PatientTreatmentProcessId, entity.HospitalId,
                document.FileName);
            SaveByteArrayToFileWithStaticMethod(userDocument.File, document.FilePath);
        }
    }

    private static void SaveByteArrayToFileWithStaticMethod(string data, string filePath)
    {
        FileInfo file = new FileInfo(filePath);
        file.Directory?.Create(); // If the directory already exists, this method does nothing.
        File.WriteAllBytes(filePath, Convert.FromBase64String(data));
    }

    [Authorize("HTS.HospitalConsultation")]
    public async Task DeleteAsync(int id)
    {
        await _hcRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="hospitalConsultation"></param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async Task IsDataValidToSave(SaveHospitalConsultationDto hospitalConsultation)
    {
        //TODO: SalesMethodAndCompanionInfo olması gerekiyor.
        //Check patient treatment process status
        if (!(await _ptpRepository.AnyAsync(p => p.Id == hospitalConsultation.PatientTreatmentProcessId
                                                 && p.TreatmentProcessStatusId == PatientTreatmentStatusEnum.NewRecord.GetHashCode())))
        {
            throw new HTSBusinessException(ErrorCode.PtpStatusNotValidToHospitalConsultation);
        }
    }


}