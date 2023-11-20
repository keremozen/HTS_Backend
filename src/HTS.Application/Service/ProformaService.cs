﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Common;
using HTS.Data.Entity;
using HTS.Dto.HTSTask;
using HTS.Dto.Proforma;
using HTS.Enum;
using HTS.Interface;
using HTS.Localization;
using HTS.PDFDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;
[Authorize]
public class ProformaService : ApplicationService, IProformaService
{
    private readonly IRepository<Proforma, int> _proformaRepository;
    private readonly IRepository<ExchangeRateInformation, int> _exchangeRateRepository;
    private readonly IRepository<Process, int> _processRepository;
    private readonly IRepository<RejectReason, int> _rejectReasonRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;
    private readonly IRepository<Operation, int> _operationRepository;
    private readonly IStringLocalizer<HTSResource> _localizer;
    private readonly IHTSTaskService _htsTaskService;

    public ProformaService(IRepository<Proforma, int> proformaRepository,
        IRepository<ExchangeRateInformation, int> exchangeRateRepository,
        IRepository<Process, int> processRepository,
        IRepository<Operation, int> operationRepository,
        IRepository<RejectReason, int> rejectReasonRepository,
        IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository,
        IStringLocalizer<HTSResource> localizer,
        IHTSTaskService htsTaskService
        )
    {
        _proformaRepository = proformaRepository;
        _exchangeRateRepository = exchangeRateRepository;
        _processRepository = processRepository;
        _rejectReasonRepository = rejectReasonRepository;
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
        _operationRepository = operationRepository;
        _localizer = localizer;
        _htsTaskService = htsTaskService;
    }

    public async Task<List<ProformaListDto>> GetNameListByOperationIdAsync(int operationId)
    {
        var query = await (await _proformaRepository.WithDetailsAsync(p => p.Creator))
            .Where(p => p.OperationId == operationId).OrderByDescending(p => p.Version).ToListAsync();
        return ObjectMapper.Map<List<Proforma>, List<ProformaListDto>>(query);
    }

    public async Task<List<ProformaPricingListDto>> GetPricingListByPTPIdAsync(int ptpId)
    {
        List<int> proformaStatuses = new List<int>
        {
            EntityEnum.ProformaStatusEnum.PatientRejected.GetHashCode(),
            EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode(),
            EntityEnum.ProformaStatusEnum.MFBWaitingApproval.GetHashCode(),
            EntityEnum.ProformaStatusEnum.MFBRejected.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode()
        };
        var query = await (await _proformaRepository.WithDetailsAsync(p => p.Creator))
            .Include(p => p.ProformaStatus)
            .Include(p => p.RejectReason)
            .Where(p => p.Operation.PatientTreatmentProcessId == ptpId
                                && proformaStatuses.Contains(p.ProformaStatusId))
            .OrderByDescending(p => p.Version)
            .ToListAsync();
        return ObjectMapper.Map<List<Proforma>, List<ProformaPricingListDto>>(query);
    }


    public async Task<ProformaDto> GetByIdAsync(int proformaId)
    {
        var query = (await _proformaRepository.WithDetailsAsync()).FirstOrDefaultAsync(p => p.Id == proformaId);
        return ObjectMapper.Map<Proforma, ProformaDto>(await query);
    }

    public async Task<int> SaveAsync(SaveProformaDto proforma)
    {
        await IsDataValidToSave(proforma);
        var operation = (await _operationRepository.WithDetailsAsync()).Include(o => o.PatientTreatmentProcess).FirstOrDefault(o => o.Id == proforma.OperationId);
        var entity = ObjectMapper.Map<SaveProformaDto, Proforma>(proforma);
        entity.IsENabiz = false;
        entity.Version = await GetVersion(entity);
        entity.CreationDate = DateTime.Now;
        if (operation.PatientTreatmentProcess.TreatmentProcessStatusId == EntityEnum.PatientTreatmentStatusEnum.ProformaCreatedWaitingForMFBApproval.GetHashCode())
        {
            entity.ProformaStatusId = EntityEnum.ProformaStatusEnum.MFBWaitingApproval.GetHashCode();

            var previousProforma = await _proformaRepository.FirstOrDefaultAsync(p => p.OperationId == proforma.OperationId && p.Version == entity.Version - 1);
            previousProforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.MFBUpdatedCancelled.GetHashCode();
            await _proformaRepository.UpdateAsync(previousProforma, true);
        }
        else
        {
            entity.ProformaStatusId = EntityEnum.ProformaStatusEnum.NewRecord.GetHashCode();
        }
        entity.ProformaCode = await GenerateProformaCode(entity.OperationId, entity.Version);
        await _proformaRepository.InsertAsync(entity, true);
        return entity.Id;
    }


    public async Task<int> SaveENabizAsync(SaveProformaDto proforma)
    {
        await IsDataValidToSave(proforma);
        var operation = (await _operationRepository.WithDetailsAsync()).Include(o => o.PatientTreatmentProcess).FirstOrDefault(o => o.Id == proforma.OperationId);
        var entity = ObjectMapper.Map<SaveProformaDto, Proforma>(proforma);
        entity.IsENabiz = true;
        entity.Version = await GetVersion(entity);
        entity.CreationDate = DateTime.Now;
        if (operation.PatientTreatmentProcess.TreatmentProcessStatusId == EntityEnum.PatientTreatmentStatusEnum.ProformaCreatedWaitingForMFBApproval.GetHashCode())
        {
            entity.ProformaStatusId = EntityEnum.ProformaStatusEnum.MFBWaitingApproval.GetHashCode();

            var previousProforma = await _proformaRepository.FirstOrDefaultAsync(p => p.OperationId == proforma.OperationId && p.Version == entity.Version - 1);
            previousProforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.MFBUpdatedCancelled.GetHashCode();
            await _proformaRepository.UpdateAsync(previousProforma, true);
        }
        else
        {
            entity.ProformaStatusId = EntityEnum.ProformaStatusEnum.NewRecord.GetHashCode();
        }
        entity.ProformaCode = await GenerateProformaCode(entity.OperationId, entity.Version);
        await _proformaRepository.InsertAsync(entity, true);
        return entity.Id;
    }

    public async Task SendAsync(int id)
    {
        //Get entity from db
        var proforma =
            (await _proformaRepository.WithDetailsAsync((p => p.Operation), (p => p.Operation.PatientTreatmentProcess)))
            .FirstOrDefault(p => p.Id == id);
        await IsDataValidToSend(proforma);
        //Treatment process, operation, proforma status update
        proforma.RejectReasonId = null;
        proforma.RejectReasonMFB = null;
        proforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.MFBWaitingApproval.GetHashCode();
        proforma.Operation.OperationStatusId =
            EntityEnum.OperationStatusEnum.ProformaCreatedWaitingForMFBApproval.GetHashCode();
        proforma.Operation.PatientTreatmentProcess.TreatmentProcessStatusId = EntityEnum.PatientTreatmentStatusEnum
            .ProformaCreatedWaitingForMFBApproval.GetHashCode();
        await _proformaRepository.UpdateAsync(proforma);
        //Close pricing task
        await _htsTaskService.CloseTask(new SaveHTSTaskDto()
        {
            HospitalId = proforma.Operation.HospitalId,
            RelatedEntityId = proforma.Operation.Id,
            TaskType = TaskTypeEnum.Pricing,
            TreatmentCode = proforma.Operation.PatientTreatmentProcess.TreatmentCode,
            PatientId = proforma.Operation.PatientTreatmentProcess.PatientId
        });
    }

    public async Task ApproveMFBAsync(int id)
    {
        //Get entity from db
        var proforma =
            (await _proformaRepository.WithDetailsAsync((p => p.Operation), (p => p.Operation.PatientTreatmentProcess)))
            .FirstOrDefault(p => p.Id == id);
        await IsDataValidToApproveMFB(proforma);
        //Treatment process, operation, proforma status update
        proforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode();
        proforma.Operation.OperationStatusId =
            EntityEnum.OperationStatusEnum.ProformaApprovedWillBeTransferredToPatient.GetHashCode();
        proforma.Operation.PatientTreatmentProcess.TreatmentProcessStatusId = EntityEnum.PatientTreatmentStatusEnum
            .ProformaApprovedWillBeTransferredToPatient.GetHashCode();
        await _proformaRepository.UpdateAsync(proforma);
        await _htsTaskService.CreateAsync(new SaveHTSTaskDto()
        {
            HospitalId = proforma.Operation.HospitalId,
            RelatedEntityId = proforma.Operation.Id,
            TaskType = TaskTypeEnum.PatientApproval,
            TreatmentCode = proforma.Operation.PatientTreatmentProcess.TreatmentCode,
            PatientId = proforma.Operation.PatientTreatmentProcess.PatientId
        });
    }

    public async Task<Object> SaveAndApproveMFBAsync(SaveProformaDto proforma)
    {
        int proformaId = await SaveAsync(proforma);
        return ApproveMFBAsync(proformaId);
    }

    public async Task RejectMFBAsync(RejectProformaDto rejectProforma)
    {
        //Get entity from db
        var proforma =
            (await _proformaRepository.WithDetailsAsync((p => p.Operation), (p => p.Operation.PatientTreatmentProcess)))
            .FirstOrDefault(p => p.Id == rejectProforma.Id);
        await IsDataValidToRejectMFB(rejectProforma, proforma);
        //Treatment process, operation, proforma status update
        proforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.MFBRejected.GetHashCode();
        proforma.Operation.OperationStatusId =
            EntityEnum.OperationStatusEnum.MFBRejectedPriceExpecting.GetHashCode();
        proforma.Operation.PatientTreatmentProcess.TreatmentProcessStatusId = EntityEnum.PatientTreatmentStatusEnum
            .MFBRejectedPriceExpecting.GetHashCode();
        proforma.RejectReasonMFB = rejectProforma.RejectReason;
        await _proformaRepository.UpdateAsync(proforma);
    }

    public async Task SendToPatient(int id)
    {
        //Get entity from db
        var proforma =
            (await _proformaRepository.WithDetailsAsync((p => p.Operation),
                (p => p.Operation.PatientTreatmentProcess),
                (p => p.Operation.PatientTreatmentProcess.Patient)))
            .FirstOrDefault(p => p.Id == id);
        IsDataValidToSendToPatient(proforma);
        var patientEmail = proforma?.Operation.PatientTreatmentProcess?.Patient.Email;
        if (string.IsNullOrEmpty(patientEmail))//No email
        {
            proforma.SendToPatientManually = true;
        }
        else
        {
            await SendEMailToPatient(proforma, patientEmail);
        }
        proforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode();
        proforma.Operation.OperationStatusId =
            EntityEnum.OperationStatusEnum.ProformaTransferredWaitingForPatientApproval.GetHashCode();
        proforma.Operation.PatientTreatmentProcess.TreatmentProcessStatusId = EntityEnum.PatientTreatmentStatusEnum
            .ProformaTransferredWaitingForPatientApproval.GetHashCode();
        await _proformaRepository.UpdateAsync(proforma);
         //Close patient approval task
        await ClosePatientApprovalTask(proforma);
    }

    private async Task SendEMailToPatient(Proforma proforma, string eMail)
    {
        //Send mail to hospital consultations
        string mailBody = $"Dear {proforma.Operation.PatientTreatmentProcess.Patient.Name} {proforma.Operation.PatientTreatmentProcess.Patient.Surname}," +
                          $"</br></br>Your application has been reviewed and the necessary treatment plan has been prepared. " +
                          $"You can find the details of your treatment process in the appendix.Proforma that is generated for your treatment is attached to email.</br>" +
        $"We wish you a nice days.</br></br>Regards.";
        var proformaReceipt = await CreateProformaPdf(proforma.Id);
        var mailSubject = $"Treatment Plan is Ready | {proforma.ProformaCode}";
        Helper.SendMail(eMail, mailBody, proformaReceipt, subject: mailSubject);
    }

    public async Task ApprovePatientAsync(int id)
    {
        //Get entity from db
        var proforma =
            (await _proformaRepository.WithDetailsAsync((p => p.Operation),
                (p => p.Operation.PatientTreatmentProcess)))
            .FirstOrDefault(p => p.Id == id);
        await IsDataValidToApprovePatient(proforma);
        //Treatment process, operation, proforma status update
        proforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode();
        proforma.Operation.OperationStatusId =
            EntityEnum.OperationStatusEnum.ProformaApprovedWaitingForPayment.GetHashCode();
        proforma.Operation.PatientTreatmentProcess.TreatmentProcessStatusId = EntityEnum.PatientTreatmentStatusEnum
            .ProformaApprovedWaitingForPayment.GetHashCode();
        await _proformaRepository.UpdateAsync(proforma);
    }

    private async Task ClosePatientApprovalTask(Proforma proforma)
    {
        await _htsTaskService.CloseTask(new SaveHTSTaskDto()
        {
            HospitalId = proforma.Operation.HospitalId,
            RelatedEntityId = proforma.Operation.Id,
            TaskType = TaskTypeEnum.PatientApproval,
            TreatmentCode = proforma.Operation.PatientTreatmentProcess.TreatmentCode,
            PatientId = proforma.Operation.PatientTreatmentProcess.PatientId
        });
    }

    public async Task RejectPatientAsync(RejectProformaDto rejectProforma)
    {
        //Get entity from db
        var proforma =
            (await _proformaRepository.WithDetailsAsync((p => p.Operation), (p => p.Operation.PatientTreatmentProcess)))
            .FirstOrDefault(p => p.Id == rejectProforma.Id);
        await IsDataValidToRejectPatient(rejectProforma, proforma);
        //Treatment process, operation, proforma status update
        proforma.ProformaStatusId = EntityEnum.ProformaStatusEnum.PatientRejected.GetHashCode();
        proforma.Operation.OperationStatusId =
            EntityEnum.OperationStatusEnum.PatientRejectedProforma.GetHashCode();
        proforma.Operation.PatientTreatmentProcess.TreatmentProcessStatusId = EntityEnum.PatientTreatmentStatusEnum
            .PatientRejectedProforma.GetHashCode();
        proforma.RejectReasonId = rejectProforma.RejectReasonId;
        await _proformaRepository.UpdateAsync(proforma);
    }

    private async Task<int> GetVersion(Proforma entity)
    {
        var query = await _proformaRepository.GetQueryableAsync();
        int version = query.Where(p => p.OperationId == entity.OperationId 
                                    && p.IsENabiz == entity.IsENabiz)
            .DefaultIfEmpty()
            .Max(p => p == null ? 0 : p.Version);
        return ++version;
    }

    private async Task<string> GenerateProformaCode(int operationId, int version)
    {
        string treatmentCode = (await _operationRepository.GetQueryableAsync()).Where(p => p.Id == operationId)
             .Select(p => p.PatientTreatmentProcess.TreatmentCode).First().ToString();
        return $"P-{treatmentCode}-{version}";
    }


    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="proforma">To be saved object</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async Task IsDataValidToSave(SaveProformaDto proforma)
    {
        //TODO:Hopsy mfd onayı bekleyen case de, sadece mfd yeni versiyon çıkabilir.
        //Status that not valid to save proforma
        //Commented because there can be proforma from enabiz
        // List<int> notSuitableStatus = new List<int>
        // {
        //     EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode(),
        //     EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode(),
        //     EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode(),
        //     EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode()
        // };

        // if (await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId
        //                                              && notSuitableStatus.Contains(p.ProformaStatusId)))
        // {
        //     throw new HTSBusinessException(ErrorCode.ProformaStatusNotValid);
        // }

        //First proforma
        if (!await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId))
        {
            //check exchange rate
            if (proforma.CurrencyId != 1)
            {
                //NOT Check the exchange rate. UI data will be used.
                // ExchangeRateInformation exchangeRateInformation = await _exchangeRateRepository.FirstOrDefaultAsync(e =>
                //       e.CurrencyId == proforma.CurrencyId && e.CreationTime.Date.Date == DateTime.Now.Date.AddDays(-1));
                // if (exchangeRateInformation == null)//No exchange rate
                // {
                //     throw new HTSBusinessException(ErrorCode.NoExchangeRateInformation);
                // }

                // if (exchangeRateInformation.ExchangeRate != proforma.ExchangeRate)
                // {
                //     throw new HTSBusinessException(ErrorCode.ExchangeRateInformationNotMatch);
                // }
            }
            else
            {
                if (proforma.ExchangeRate != 1)
                {
                    throw new HTSBusinessException(ErrorCode.ExchangeRateInformationNotMatch);
                }
            }
        }
        else
        {//More than 1 revision
         //All revisions will use same exchange rate
         // if ((await _proformaRepository.FirstOrDefaultAsync(p => p.OperationId == proforma.OperationId)).ExchangeRate !=
         //     proforma.ExchangeRate)
         // {
         //     throw new HTSBusinessException(ErrorCode.ExchangeRateInformationNotMatch);
         // }

            //Only last version can be updated
            /*int maxVersion = (await _proformaRepository.GetListAsync(p => p.OperationId == proforma.OperationId)).Max(p => p.Version);
            if (proforma.Version != maxVersion)
            {
                throw new HTSBusinessException(ErrorCode.LastProformaVersionCanBeOperated);
            }*/
        }
        //Check additional service data
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

        //Get process prices
        var processIds = proforma.ProformaProcesses.Select(p => p.ProcessId).ToList();
        var processes = (await _processRepository.WithDetailsAsync(p => p.ProcessCosts)).Where(b => b.IsActive == true
        && processIds.Contains(b.Id)).ToList();
        //Check each process price
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
                || Math.Round(Decimal.Divide(proformaProcess.TotalPrice, proforma.ExchangeRate), 2) != proformaProcess.ProformaPrice
                || (proformaProcess.Change != 0 &&
                    Math.Abs(Math.Round((proformaProcess.ProformaPrice + Decimal.Divide(proformaProcess.ProformaPrice * proformaProcess.Change, 100)), 2) - proformaProcess.ProformaFinalPrice) > 1)
                || (proformaProcess.Change == 0 &&
                    Math.Abs(Math.Round(proformaProcess.ProformaPrice, 2) - proformaProcess.ProformaFinalPrice) > 1 ))
            {
                throw new HTSBusinessException(ErrorCode.InvalidCalculationsInProforma);
            }
        }

        if (proforma.TotalProformaPrice != proforma.ProformaProcesses.Sum(p => p.ProformaFinalPrice))
        {
            throw new HTSBusinessException(ErrorCode.InvalidCalculationsInProforma);
        }
    }


    /// <summary>
    /// Checks if data is valid to send to mfb
    /// </summary>
    /// <param name="proforma">To be send proforma</param>
    /// <exception cref="HTSBusinessException"></exception>
    private async Task IsDataValidToSend(Proforma proforma)
    {
        if (proforma == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }
        //Status that not valid to send proforma
        List<int> notSuitableStatus = new List<int>
        {
            EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode(),
            EntityEnum.ProformaStatusEnum.MFBWaitingApproval.GetHashCode()
        };

        if (await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId
                                                    && notSuitableStatus.Contains(p.ProformaStatusId)))
        {
            throw new HTSBusinessException(ErrorCode.ProformaStatusNotValid);
        }
        //Only last version can be updated
        int maxVersion = (await _proformaRepository.GetListAsync(p => p.OperationId == proforma.OperationId)).Max(p => p.Version);
        if (proforma.Version != maxVersion)
        {
            throw new HTSBusinessException(ErrorCode.LastProformaVersionCanBeOperated);
        }
    }

    /// <summary>
    /// Checks if data is valid to approve mfb
    /// </summary>
    /// <param name="proforma">To be approved proforma</param>
    /// <exception cref="HTSBusinessException"></exception>
    private async Task IsDataValidToApproveMFB(Proforma proforma)
    {
        if (proforma == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }
        //Status that not valid to send proforma
        List<int> notSuitableStatus = new List<int>
        {
            EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode()
        };

        if (await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId
                                                    && notSuitableStatus.Contains(p.ProformaStatusId))
            || proforma.ProformaStatusId != EntityEnum.ProformaStatusEnum.MFBWaitingApproval.GetHashCode())
        {
            throw new HTSBusinessException(ErrorCode.ProformaStatusNotValid);
        }
        //Sadece son versiyon onaylanıp reddedilebilir
        /*int maxVersion = (await _proformaRepository.GetListAsync(p => p.OperationId == proforma.OperationId)).Max(p => p.Version);
        if (proforma.Version != maxVersion)
        {
            throw new HTSBusinessException(ErrorCode.LastProformaVersionCanBeApprovedRejected);
        }*/
    }

    /// <summary>
    /// Checks if data is valid to reject mfb
    /// </summary>
    /// <param name="rejectProforma">Reject object</param>
    /// <param name="proforma">To be rejected proforma</param>
    /// <exception cref="HTSBusinessException"></exception>
    private async Task IsDataValidToRejectMFB(RejectProformaDto rejectProforma, Proforma proforma)
    {
        if (proforma == null
            || string.IsNullOrEmpty(rejectProforma.RejectReason))
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }

        //Status that not valid to reject proforma
        List<int> notSuitableStatus = new List<int>
        {
            EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode()
        };

        if (await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId
                                                    && notSuitableStatus.Contains(p.ProformaStatusId))
            || proforma.ProformaStatusId != EntityEnum.ProformaStatusEnum.MFBWaitingApproval.GetHashCode())
        {
            throw new HTSBusinessException(ErrorCode.ProformaStatusNotValid);
        }

        //Sadece son versiyon onaylanıp reddedilebilir
        int maxVersion = (await _proformaRepository.GetListAsync(p => p.OperationId == proforma.OperationId)).Max(p => p.Version);
        if (proforma.Version != maxVersion)
        {
            throw new HTSBusinessException(ErrorCode.LastProformaVersionCanBeApprovedRejected);
        }
    }

    /// <summary>
    /// Checks if data is valid to send to patient approval
    /// </summary>
    /// <param name="proforma">Current proforma in db</param>
    /// <exception cref="HTSBusinessException"></exception>
    private void IsDataValidToSendToPatient(Proforma proforma)
    {
        if (proforma == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }

        if (proforma.ProformaStatusId != EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode())
        {
            throw new HTSBusinessException(ErrorCode.ProformaStatusNotValid);
        }
    }

    /// <summary>
    /// Checks if data is valid to patient approve
    /// </summary>
    /// <param name="proforma">Proforma in db</param>
    /// <exception cref="NotImplementedException"></exception>
    private async Task IsDataValidToApprovePatient(Proforma proforma)
    {
        if (proforma == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }
        //Status that not valid to approve proforma
        List<int> notSuitableStatus = new List<int>
        {
            EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode()
        };

        if (await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId
                                                    && notSuitableStatus.Contains(p.ProformaStatusId))
            || proforma.ProformaStatusId != EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode())
        {
            throw new HTSBusinessException(ErrorCode.ProformaStatusNotValid);
        }
        //Sadece son versiyon onaylanıp reddedilebilir
        int maxVersion = (await _proformaRepository.GetListAsync(p => p.OperationId == proforma.OperationId)).Max(p => p.Version);
        if (proforma.Version != maxVersion)
        {
            throw new HTSBusinessException(ErrorCode.LastProformaVersionCanBeApprovedRejected);
        }
    }



    /// <summary>
    /// Checks if data is valid to patient reject
    /// </summary>
    /// <param name="rejectProforma">Reject object</param>
    /// <param name="proforma">To be rejected proforma</param>
    /// <exception cref="HTSBusinessException"></exception>
    private async Task IsDataValidToRejectPatient(RejectProformaDto rejectProforma, Proforma proforma)
    {
        if (proforma == null)
        {
            throw new HTSBusinessException(ErrorCode.BadRequest);
        }

        if (!await _rejectReasonRepository.AnyAsync(r => r.IsActive && r.Id == rejectProforma.RejectReasonId))
        {
            throw new HTSBusinessException(ErrorCode.RelationalDataIsMissing);
        }

        //Status that not valid to reject proforma
        List<int> notSuitableStatus = new List<int>
        {
            EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WaitingForPayment.GetHashCode(),
            EntityEnum.ProformaStatusEnum.WillBeTransferedToPatient.GetHashCode()
        };

        if (await _proformaRepository.AnyAsync(p => p.OperationId == proforma.OperationId
                                                    && notSuitableStatus.Contains(p.ProformaStatusId))
            || proforma.ProformaStatusId != EntityEnum.ProformaStatusEnum.WaitingForPatientApproval.GetHashCode())
        {
            throw new HTSBusinessException(ErrorCode.ProformaStatusNotValid);
        }

        //Sadece son versiyon onaylanıp reddedilebilir
        int maxVersion = (await _proformaRepository.GetListAsync(p => p.OperationId == proforma.OperationId)).Max(p => p.Version);
        if (proforma.Version != maxVersion)
        {
            throw new HTSBusinessException(ErrorCode.LastProformaVersionCanBeApprovedRejected);
        }
    }

    public async Task<byte[]> CreateProformaPdf(int id)
    {
        byte[] bytes = null;
        var proforma = (await _proformaRepository.WithDetailsAsync()).FirstOrDefault(p => p.Id == id);
        ProformaDocument document = null;
        if (proforma != null)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;
            document = new ProformaDocument(proforma);
            bytes = document.GeneratePdf();
        }
        return bytes;
    }



}