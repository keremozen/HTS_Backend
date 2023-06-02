using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Interface;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class HospitalConsultationService : ApplicationService, IHospitalConsultationService
{
    private readonly IRepository<HospitalConsultation, int> _hcRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _ptpRepository;

    public HospitalConsultationService(IRepository<HospitalConsultation, int> hcRepository,
        IRepository<PatientTreatmentProcess, int> ptpRepository)
    {
        _hcRepository = hcRepository;
        _ptpRepository = ptpRepository;
    }

    public async Task<HospitalConsultationDto> GetAsync(int id)
    {
        var query = (await _hcRepository.WithDetailsAsync()).Where(p => p.Id == id);
        var consultation = await AsyncExecuter.FirstOrDefaultAsync(query);
        return ObjectMapper.Map<HospitalConsultation, HospitalConsultationDto>(consultation);
    }
    public async Task<PagedResultDto<HospitalConsultationDto>> GetByPatientTreatmenProcessAsync(int ptpId)
    {
        var query = await _hcRepository.WithDetailsAsync();
        query = query.Where(hc => hc.PatientTreatmentProcessId == ptpId);

        var responseList = ObjectMapper.Map<List<HospitalConsultation>, List<HospitalConsultationDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _hcRepository.CountAsync();//item count

        return new PagedResultDto<HospitalConsultationDto>(totalCount, responseList);
    }


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

        //Update patient treatment process entity status clm - "Hastanelere Danışıldı - Cevap Bekleniyor"
        var ptpEntity = (await _ptpRepository.GetQueryableAsync()).FirstOrDefault(ptp => ptp.Id == hospitalConsultation.PatientTreatmentProcessId);
        if (ptpEntity != null)
        {
            ptpEntity.TreatmentProcessStatusId = PatientTreatmentStatusEnum.HospitalAskedWaitingResponse.GetHashCode();
            await _ptpRepository.UpdateAsync(ptpEntity);
        }

        await _hcRepository.InsertManyAsync(entityList);
        //TODO: Her hospital'ın sorumlusuna (sorumlularına olabilir?) mail atılacak. 
    }

    private async Task<int> GetRowNumber(int ptProcessId)
    {
        var query = await _hcRepository.GetQueryableAsync();
        int rowNumber = query.Where(hc => hc.PatientTreatmentProcessId == ptProcessId).Max(hc => (int?)hc.RowNumber) ?? 0;
        return ++rowNumber;
    }

    private void ProcessHospitalConsultationDocuments(HospitalConsultation entity, SaveHospitalConsultationDto hospitalConsultation)
    {
        //TODO:Hopsy update this method
        foreach (var document in entity.HospitalConsultationDocuments)
        {
            document.FilePath = "To Be Removed";
            var userDocument = hospitalConsultation.HospitalConsultationDocuments.FirstOrDefault(d => d.FileName == document.FileName);
            SaveByteArrayToFileWithStaticMethod(userDocument.File, document.FilePath);
        }
    }

    private static void SaveByteArrayToFileWithStaticMethod(string data, string filePath)
    {
        File.WriteAllBytes(filePath, Convert.FromBase64String(data));
    }


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