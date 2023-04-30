using System;
using System.Collections.Generic;
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
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class HospitalConsultationService : ApplicationService,IHospitalConsultationService
{
    private readonly IRepository<HospitalConsultation, int> _hcRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _ptpRepository;
    
    public HospitalConsultationService(IRepository<HospitalConsultation, int> hcRepository,
        IRepository<PatientTreatmentProcess, int> ptpRepository)
    {
        _hcRepository = hcRepository;
        _ptpRepository = ptpRepository;
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
        List<HospitalConsultation> entityList = new List<HospitalConsultation>();
        HospitalConsultation entity;
        foreach (var hospital in hospitalConsultation.HospitalIds)
        {
            entity = ObjectMapper.Map<SaveHospitalConsultationDto, HospitalConsultation>(hospitalConsultation);
            entity.HospitalId = hospital;
            entity.HospitalConsultationStatusId = HospitalConsultationStatusEnum.HospitalResponseWaiting.GetHashCode();
            entityList.Add(entity);
        }
        await _hcRepository.InsertManyAsync(entityList);
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
        //Check patient treatment process status
        if (!(await _ptpRepository.AnyAsync(p => p.Id == hospitalConsultation.PatientTreatmentProcessId
                                                 && p.TreatmentProcessStatusId == PatientTreatmentStatusEnum.NewRecord.GetHashCode())))
        {
            throw new HTSBusinessException(ErrorCode.PTPStatusNotValidToHospitalConsultation);
        }
    }
   

}