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
using HTS.Enum;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class PatientTreatmentProcessService : ApplicationService, IPatientTreatmentProcessService
{
    private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;
    private readonly IRepository<Proforma, int> _proformaRepository;
    private readonly IIdentityUserRepository _userRepository;
    private readonly IUSSService _ussService;
    public PatientTreatmentProcessService(IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository,
        IRepository<Proforma, int> proformaRepository,
        IIdentityUserRepository userRepository,
        IUSSService ussService)
    {
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
        _userRepository = userRepository;
        _ussService = ussService;
        _proformaRepository = proformaRepository;
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
        await IsDataValidToSetSysTrackingNumber(treatmentCode);
        ExternalApiResult result = await _ussService.GetSysTrackingNumber(treatmentCode);
        if (result.durum != 1)
        {
            return false;
        }
        List<GetSysTrackingNumberObject> trackingNumbers = (List<GetSysTrackingNumberObject>)result.sonuc;
        if (trackingNumbers?.Any() ?? false )
        {
            var lastTrackingNumber = trackingNumbers.Last();

            var ptp = await _patientTreatmentProcessRepository.FirstOrDefaultAsync(ptp =>
                ptp.TreatmentCode == treatmentCode);
         //   ptp.SysTrackingNumber = lastTrackingNumber.sysTakipNo;
            await _patientTreatmentProcessRepository.UpdateAsync(ptp);
            return true;
        }
        return false;
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
    
    /// <summary>
    /// Checks if data is valid to set systrackingnumber
    /// </summary>
    /// <param name="treatmentCode">Treatment code</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async Task IsDataValidToSetSysTrackingNumber(string treatmentCode)
    {

        //Check proforma
        var proforma =  (await _proformaRepository.GetQueryableAsync())
            .FirstOrDefault(p => p.Operation.PatientTreatmentProcess.TreatmentCode == treatmentCode
                                 && p.ProformaStatusId == ProformaStatusEnum.PaymentCompleted.GetHashCode());
        if (proforma == null)
        {//No payment completed proforma in db
            throw new HTSBusinessException(ErrorCode.NoPaymentCompletedProforma);
        }
     
    }
}