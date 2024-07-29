using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
    private readonly IRepository<ENabizProcess, int> _eNabizProcessRepository;
    private readonly IRepository<ProcessCost, int> _pCostRepository;
    private readonly IIdentityUserRepository _userRepository;
    private readonly IUSSService _ussService;

    public PatientTreatmentProcessService(IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository,
        IRepository<Proforma, int> proformaRepository,
        IRepository<ENabizProcess, int> eNabizProcessRepository,
        IRepository<ProcessCost, int> pCostRepository,
        IIdentityUserRepository userRepository,
        IUSSService ussService)
    {
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
        _userRepository = userRepository;
        _ussService = ussService;
        _proformaRepository = proformaRepository;
        _eNabizProcessRepository = eNabizProcessRepository;
        _pCostRepository = pCostRepository;
    }

    [Authorize]
    public async Task<PagedResultDto<PatientTreatmentProcessDetailedDto>> GetListByPatientIdAsync(int patientId)
    {
        //Get all entities
        var patientTreatmentProcesses = await (await _patientTreatmentProcessRepository.WithDetailsAsync())
            .AsNoTracking()
            .Where(t => t.PatientId == patientId)
            .ToListAsync();
        var proformas = await (await _proformaRepository.GetQueryableAsync()).AsNoTracking()
            .Include(p => p.ProformaProcesses)
            .Include(p => p.Operation)
            .Include(p => p.Payments)
            .ThenInclude(p => p.PaymentItems)
            .Where(p => patientTreatmentProcesses.Select(t => t.Id)
                            .Contains(p.Operation.PatientTreatmentProcessId.Value)
                        && (p.ProformaStatusId == ProformaStatusEnum.WaitingForPayment.GetHashCode()
                            || p.ProformaStatusId == ProformaStatusEnum.PaymentCompleted.GetHashCode()))
            .ToListAsync();
        //Group to get latest proforma
        var groupList = from p in proformas
            group p by p.OperationId
            into g
            select new
            {
                Version = proformas.Where(p => p.OperationId == g.Key).Max(p => p.Version),
                OperationId = g.Key,
            };
        proformas = proformas
            .Where(p => groupList.Any(pp => pp.OperationId == p.OperationId && pp.Version == p.Version)).ToList();

        var eNabizList = await (await _eNabizProcessRepository.GetQueryableAsync()).AsNoTracking()
            .Include(e => e.Process)
            .ThenInclude(p => p.ProcessCosts)
            .Include(e => e.Process)
            .ThenInclude(p => p.ProcessRelationChildren)
            // .Include(e => e.Process)
            // .ThenInclude(p => p.ProcessRelations)
            .Where(e => patientTreatmentProcesses.Select(t => t.TreatmentCode).Contains(e.TreatmentCode)
                        && e.ProcessId != null)
            .ToListAsync();


        var responseList = new List<PatientTreatmentProcessDetailedDto>();
        foreach (var ptp in patientTreatmentProcesses)
        {
            var response = ObjectMapper.Map<PatientTreatmentProcess, PatientTreatmentProcessDetailedDto>(ptp);
            response.ProformaPrice = proformas.Where(p => p.Operation.PatientTreatmentProcessId == ptp.Id)
                .Sum(p => p.TotalProformaPrice);
            response.PaymentPrice = proformas.Where(p => p.Operation.PatientTreatmentProcessId == ptp.Id)
                .SelectMany(p => p.Payments)
                .Sum(p => p.PaymentItems.Sum(i => i.Price * i.ExchangeRate));
            response.UnPaidPrice = response.ProformaPrice - response.PaymentPrice;
            response.HBYSPrice = await SetENabizPrice(ptp, eNabizList);
            responseList.Add(response);
        }

        var totalCount = responseList.Count(); //item count
        return new PagedResultDto<PatientTreatmentProcessDetailedDto>(totalCount, responseList);
    }

    private async Task<decimal> SetENabizPrice(PatientTreatmentProcess ptp,
        List<ENabizProcess> eNabizProcesses)
    {
        eNabizProcesses = eNabizProcesses.Where(p => p.TreatmentCode == ptp.TreatmentCode).ToList();
        Dictionary<int, int> responseLookUp = new Dictionary<int, int>();
        foreach (var eNabizProcess in eNabizProcesses)
        {
            int eNabizCount = string.IsNullOrEmpty(eNabizProcess.ADET) ? 0 : Convert.ToInt32(eNabizProcess.ADET);

            if (responseLookUp.ContainsKey(eNabizProcess.ProcessId.Value))
            {
                responseLookUp[eNabizProcess.ProcessId.Value] += eNabizCount;
            }
            else
            {
                responseLookUp.Add(eNabizProcess.ProcessId.Value, eNabizCount);
            }
        }

        if (responseLookUp.Any())
        {
            var processCosts = await (await _pCostRepository.GetQueryableAsync())
                .Where(p => responseLookUp.Keys.Contains(p.ProcessId))
                .ToListAsync();
            var cost = responseLookUp.Sum(l =>
                l.Value * (processCosts.FirstOrDefault(c => c.ProcessId == l.Key)).UshasPrice);
            return cost;
        }

        return 0;
    }

    [Authorize]
    public async Task<PatientTreatmentProcessDto> GetByPatientTreatmentProcessIdAsync(int patientTreatmentProcessId)
    {
        //Get all entities
        var query = (await _patientTreatmentProcessRepository.WithDetailsAsync())
            .FirstOrDefault(t => t.Id == patientTreatmentProcessId);
        return ObjectMapper.Map<PatientTreatmentProcess, PatientTreatmentProcessDto>(query);
    }

    [Authorize("HTS.PatientManagement")]
    public async Task<PatientTreatmentProcessDto> StartAsync(int patientId)
    {
        var entity = new PatientTreatmentProcess()
        {
            PatientId = patientId,
            TreatmentProcessStatusId = PatientTreatmentStatusEnum.NewRecord.GetHashCode(),
            TreatmentCode = await GenerateTreatmentNumber()
        };
        await _patientTreatmentProcessRepository.InsertAsync(entity, true);
        var newEntityQuery =
            (await _patientTreatmentProcessRepository.WithDetailsAsync()).Where(p => p.Id == entity.Id);
        return ObjectMapper.Map<PatientTreatmentProcess, PatientTreatmentProcessDto>(
            await AsyncExecuter.FirstOrDefaultAsync(newEntityQuery));
    }

    [Authorize("HTS.PatientManagement")]
    public async Task FinalizeAsync(int id, FinalizePtpDto finalizePtp)
    {
        // Fetch the entity from the repository
        var entity = await _patientTreatmentProcessRepository.GetAsync(id);
        IsDataValidToFinalize(entity);
     
        // Update the entity with the finalization details
        entity.IsFinalized = true;
        entity.FinalizationTypeId = finalizePtp.FinalizationTypeId;
        entity.FinalizationDescription = finalizePtp.Description;

        // Save the changes to the repository
        await _patientTreatmentProcessRepository.UpdateAsync(entity);
    }
    
    /// <summary>
    /// Checks if data is valid to finalize
    /// </summary>
    /// <param name="entity">To be finalized entity</param>
    /// <exception cref="HTSBusinessException"></exception>
    private void IsDataValidToFinalize(PatientTreatmentProcess entity)
    {
        if (entity == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }

        if (entity.IsFinalized)
        {
            throw new HTSBusinessException(ErrorCode.PtpAlreadyFinalized);
        }
    }


    [Authorize("HTS.PatientManagement")]
    public async Task DeFinalizeAsync(int id)
    {
        // Fetch the entity from the repository
        var entity = await _patientTreatmentProcessRepository.GetAsync(id);
        IsDataValidToDeFinalize(entity);

        // Update the entity with the definalization details
        entity.IsFinalized = false;
        entity.FinalizationTypeId = null;
        entity.FinalizationDescription = null;

        // Save the changes to the repository
        await _patientTreatmentProcessRepository.UpdateAsync(entity);

    }
    
    /// <summary>
    /// Checks if data is valid to de finalize
    /// </summary>
    /// <param name="entity">To be de finalized entity</param>
    /// <exception cref="HTSBusinessException"></exception>
    private void IsDataValidToDeFinalize(PatientTreatmentProcess entity)
    {
        if (entity == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }

        if (!entity.IsFinalized)
        {
            throw new HTSBusinessException(ErrorCode.PtpAlreadyDeFinalized);
        }
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

            bool isUnique =
                !(await _patientTreatmentProcessRepository.AnyAsync(t =>
                    t.TreatmentCode == treatmentNumberBuilder.ToString()));
            if (isUnique)
            {
                return treatmentNumberBuilder.ToString();
            }
        }

        throw new HTSBusinessException(ErrorCode.TreatmentNumberCouldNotBeGenerated);
    }
}