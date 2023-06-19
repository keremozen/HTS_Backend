using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.Proforma;
using HTS.Enum;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Polly.Fallback;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace HTS.Service;
[Authorize]
public class ProformaService : ApplicationService, IProformaService
{
    private readonly IRepository<Proforma, int> _proformaRepository;
    private readonly IRepository<ExchangeRateInformation, int> _exchangeRateRepository;
    private readonly IRepository<Process, int> _processRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;

    public ProformaService(IRepository<Proforma, int> proformaRepository,
        IRepository<ExchangeRateInformation, int> exchangeRateRepository,
        IRepository<Process, int> processRepository,
        IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository)
    {
        _proformaRepository = proformaRepository;
        _exchangeRateRepository = exchangeRateRepository;
        _processRepository = processRepository;
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
    }

    public async Task<List<ProformaListDto>> GetNameListByOperationIdAsync(int operationId)
    {
        var query = await (await _proformaRepository.WithDetailsAsync(p => p.Creator))
            .Where(p => p.OperationId == operationId).ToListAsync();
       return ObjectMapper.Map<List<Proforma>, List<ProformaListDto>>(query);
    }
    


    public async Task SaveAsync(SaveProformaDto proforma)
    {
        IsDataValidToSave(proforma);
        var entity = ObjectMapper.Map<SaveProformaDto, Proforma>(proforma);
        entity.Version = await GetVersion(entity);;
        entity.CreationDate = DateTime.Now;
        entity.ProformaStatusId = EntityEnum.ProformaStatusEnum.NewRecord.GetHashCode();
        entity.ProformaCode = await GenerateProformaCode(entity.OperationId, entity.Version);
        await _proformaRepository.InsertAsync(entity);
    }

    private async Task<int> GetVersion(Proforma entity)
    {
        var query = await _proformaRepository.GetQueryableAsync();
        int version = query.Where(p => p.OperationId == entity.OperationId)
            .DefaultIfEmpty()
            .Max(p => p == null ? 0 : p.Version);
        return ++version;
    }

    private async Task<string> GenerateProformaCode(int operationId, int version)
    {
       string treatmentCode = (await _proformaRepository.GetQueryableAsync()).Where(p => p.OperationId == operationId)
            .Select(p => p.Operation.PatientTreatmentProcess.TreatmentCode).ToString();
       return $"P-{treatmentCode}-{version}";
    }
    

    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="proforma">To be saved object</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async void IsDataValidToSave(SaveProformaDto proforma)
    {
        //Status that not valid to save proforma
        List<int> notSuitableStatus = new List<int>
        {
            EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode()
        };
        
        if (!await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId 
                                                     && notSuitableStatus.Contains(p.ProformaStatusId)))
        {
            throw new HTSBusinessException(ErrorCode.ProformaStatusNotValid);
        }

        //First proforma
        if (!await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId))
        {
            //check exchange rate
          ExchangeRateInformation exchangeRateInformation =  await _exchangeRateRepository.GetAsync(e =>
                e.CreationTime.Date.Date == DateTime.Now.Date.AddDays(-1));
          if (exchangeRateInformation == null)//No exchange rate
          {
              throw new HTSBusinessException(ErrorCode.NoExchangeRateInformation);
          }

          if (exchangeRateInformation.ExchangeRate != proforma.ExchangeRate)
          {
              throw new HTSBusinessException(ErrorCode.ExchangeRateInformationNotMatch);
          }
        }
        else
        {//More than 1 revision
            if ((await _proformaRepository.GetAsync(p => p.OperationId == proforma.OperationId)).ExchangeRate !=
                proforma.ExchangeRate)
            {
                throw new HTSBusinessException(ErrorCode.ExchangeRateInformationNotMatch);
            }
        }
        //Check data
        bool invalidData = false;
        foreach (var additionalService in proforma.ProformaAdditionalServices)
        {
            if (invalidData)
            {
                throw new HTSBusinessException(ErrorCode.ProformaAdditionalServiceNotValid);
            }
            switch (additionalService.AdditionalServiceId)
            {
                case EntityEnum.AdditionalServiceEnum.ServiceAdmission:
                    if (additionalService.DayCount == null 
                        || additionalService.DayCount.Value < 1
                        || additionalService.RoomTypeId == null)
                    {
                        invalidData = true;
                    }
                    break;
                case EntityEnum.AdditionalServiceEnum.IntensiveCare:
                    if (additionalService.DayCount == null 
                        || additionalService.DayCount.Value < 1)
                    {
                        invalidData = true;
                    }
                    break;
                case EntityEnum.AdditionalServiceEnum.Accomodation:
                    if (additionalService.DayCount == null 
                        || additionalService.DayCount.Value < 1
                        || additionalService.CompanionCount < 1)
                    {
                        invalidData = true;
                    }
                    break;
                case EntityEnum.AdditionalServiceEnum.Trip:
                    if (additionalService.CompanionCount < 0)
                    {
                        invalidData = true;
                    }
                    break;
                case EntityEnum.AdditionalServiceEnum.PhysicalExamination:
                    if (additionalService.ItemCount == null 
                        || additionalService.ItemCount.Value < 1)
                    {
                        invalidData = true;
                    }
                    break;
            }
        }

       var processIds= proforma.ProformaProcesses.Select(p => p.ProcessId).ToList();
       var processes = (await _processRepository.WithDetailsAsync(p => p.ProcessCosts)).Where(b => b.IsActive == true 
       && processIds.Contains(b.Id)).ToList();
        foreach (var proformaProcess in proforma.ProformaProcesses)
        {
            var process = processes.FirstOrDefault(p => p.Id == proformaProcess.ProcessId);
            if (process == null)
            {
                throw new HTSBusinessException(ErrorCode.InvalidProcessInProforma);
            }

            DateTime today = DateTime.Now.Date;
            if (!process.ProcessCosts.Any(c => c.ValidityStartDate.Date <= today 
                && c.ValidityEndDate >= today
                && c.UshasPrice == proformaProcess.UnitPrice))
            {
                throw new HTSBusinessException(ErrorCode.InvalidProcessUnitPriceInProforma);
            }

            if ((proformaProcess.UnitPrice * proformaProcess.TreatmentCount) != proformaProcess.TotalPrice
                || Decimal.Divide(proformaProcess.TotalPrice,proforma.ExchangeRate) != proformaProcess.ProformaPrice
                || (proformaProcess.Change != 0 &&  
                    Math.Round((proformaProcess.ProformaPrice + Decimal.Divide(proformaProcess.ProformaPrice*proformaProcess.Change,100)),2) != proformaProcess.ProformaFinalPrice )
                || (proformaProcess.Change == 0 &&  
                    Math.Round(proformaProcess.ProformaPrice,2) != proformaProcess.ProformaFinalPrice))
            {
                throw new HTSBusinessException(ErrorCode.InvalidCalculationsInProforma);
            }
        }

        if (proforma.TotalProformaPrice != proforma.ProformaProcesses.Sum(p => p.ProformaFinalPrice))
        {
            throw new HTSBusinessException(ErrorCode.InvalidCalculationsInProforma);
        }
    }

}