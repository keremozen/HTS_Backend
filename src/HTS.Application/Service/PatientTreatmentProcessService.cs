using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientNote;
using HTS.Dto.PatientTreatmentProcess;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class PatientTreatmentProcessService : ApplicationService, IPatientTreatmentProcessService
{
    private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;
    private readonly IIdentityUserRepository _userRepository;
    public PatientTreatmentProcessService(IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository,
        IIdentityUserRepository userRepository)
    {
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
        _userRepository = userRepository;
    }

    
    public async Task<PatientTreatmentProcessDto> StartAsync(int patientId)
    {
        var entity = new PatientTreatmentProcess()
        {
            PatientId = patientId,
            TreatmentProcessStatusId = PatientTreatmentStatusEnum.NewRecord.GetHashCode(),
            TreatmentCode = Guid.NewGuid().ToString()
        };
        await _patientTreatmentProcessRepository.InsertAsync(entity,true);
        return ObjectMapper.Map<PatientTreatmentProcess, PatientTreatmentProcessDto>(entity);
    }
}