using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.External;
using HTS.Dto.Hospital;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientNote;
using HTS.Dto.PatientTreatmentProcess;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IUSSService _ussService;
    public PatientTreatmentProcessService(IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository,
        IIdentityUserRepository userRepository,
        IUSSService ussService)
    {
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
        _userRepository = userRepository;
        _ussService = ussService;
    }

    [Authorize]
    public async Task<PagedResultDto<PatientTreatmentProcessDto>> GetListByPatientIdAsync(int patientId)
    {
        //Get all entities
        var query = (await _patientTreatmentProcessRepository.WithDetailsAsync())
                                                .Where(t => t.PatientId == patientId);
        var responseList = ObjectMapper.Map<List<PatientTreatmentProcess>, List<PatientTreatmentProcessDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = responseList.Count();//item count
        return new PagedResultDto<PatientTreatmentProcessDto>(totalCount, responseList);
    }

    [Authorize("HTS.PatientManagement")]
    public async Task<PatientTreatmentProcessDto> StartAsync(int patientId)
    {
        var entity = new PatientTreatmentProcess()
        {
            PatientId = patientId,
            TreatmentProcessStatusId = PatientTreatmentStatusEnum.NewRecord.GetHashCode(),
            TreatmentCode = await  GenerateTreatmentNumber()
        };
        await _patientTreatmentProcessRepository.InsertAsync(entity, true);
        var newEntityQuery = (await _patientTreatmentProcessRepository.WithDetailsAsync()).Where(p => p.Id == entity.Id);
        return ObjectMapper.Map<PatientTreatmentProcess, PatientTreatmentProcessDto>(await AsyncExecuter.FirstOrDefaultAsync(newEntityQuery));
    }
    
    public async Task<bool> SetSysTrackingNumber(string treatmentCode)
    {
        ExternalApiResult result = await _ussService.GetSysTrackingNumber(treatmentCode);
        return true;
    }

    /// <summary>
    /// Try to generate unique 10 character treatment code in UNNNNNNNNN format
    /// </summary>
    /// <returns>Generated treatment code</returns>
    /// <exception cref="HTSBusinessException"></exception>
    private async Task<string> GenerateTreatmentNumber()
    {
        Random rnd = new Random();
        int generateCount = 0;
        while (generateCount++ < 11)
        {
            var treatmentNumberBuilder = new StringBuilder(10);
            treatmentNumberBuilder.Append("U");
            for (int i = 0; i < 9; i++)
            {
                treatmentNumberBuilder.Append(rnd.Next(0, 9));
            }
            bool isUnique = !(await _patientTreatmentProcessRepository.AnyAsync(t => t.TreatmentCode == treatmentNumberBuilder.ToString()));
            if (isUnique)
            {
                return treatmentNumberBuilder.ToString();
            }
        }
        throw new HTSBusinessException(ErrorCode.TreatmentNumberCouldNotBeGenerated);
    }
}