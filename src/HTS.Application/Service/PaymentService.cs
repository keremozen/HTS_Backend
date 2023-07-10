using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.Patient;
using HTS.Dto.Payment;
using HTS.Dto.Process;
using HTS.Enum;
using HTS.Interface;
using HTS.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Localization;
using Volo.Abp.Users;

namespace HTS.Service;
[Authorize]
public class PaymentService : ApplicationService, IPaymentService
{
    private readonly IRepository<Payment, int> _paymentRepository;
    private readonly IRepository<PaymentItem, int> _paymentItemRepository;
    private readonly IRepository<Proforma, int> _proformaRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _ptpRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IRepository<Hospital, int> _hospitalRepository;
    private readonly IRepository<ExchangeRateInformation, int> _erRepository;


    public PaymentService(IRepository<Payment, int> paymentRepository,
        IRepository<PaymentItem, int> paymentItemRepository,
        IRepository<Proforma, int> proformaRepository,
        IRepository<PatientTreatmentProcess, int> ptpRepository,
        IRepository<Hospital, int> hospitalRepository,
        IRepository<ExchangeRateInformation, int> erRepository,
        ICurrentUser currentUser
        )
    {
        _paymentRepository = paymentRepository;
        _paymentItemRepository = paymentItemRepository;
        _proformaRepository = proformaRepository;
        _ptpRepository = ptpRepository;
        _hospitalRepository = hospitalRepository;
        _erRepository = erRepository;
        _currentUser = currentUser;
    }

    public async Task<PaymentDto> GetAsync(int id)
    {
        var query = (await _paymentRepository.WithDetailsAsync()).Where(p => p.Id == id);
        var payment = await AsyncExecuter.FirstOrDefaultAsync(query);
        return ObjectMapper.Map<Payment, PaymentDto>(payment);
    }
    public async Task<PagedResultDto<ListPaymentDto>> GetListAsync(int ptpId)
    {
        //Get all entities
        var query = (await _paymentRepository.WithDetailsAsync())
            .Where(p => p.PtpId == ptpId);
        var responseList = ObjectMapper.Map<List<Payment>, List<ListPaymentDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _paymentRepository.CountAsync();//item count
        return new PagedResultDto<ListPaymentDto>(totalCount, responseList);
    }

    public async Task CreateAsync(SavePaymentDto payment)
    {
        await IsDataValidToCreate(payment);
        var entity = ObjectMapper.Map<SavePaymentDto, Payment>(payment);
        entity.PaymentDate = DateTime.Now;
        entity.CollectorNameSurname = $"{_currentUser.Name} {_currentUser.SurName}";
        if (payment.ProformaId.HasValue)//Set hospitalid and proforma number from proforma
        {
            var query = (await _proformaRepository.WithDetailsAsync((p => p.Operation),
                (p => p.Operation.HospitalResponse),
                (p => p.Operation.HospitalResponse.HospitalConsultation)))
                .Where(p => p.Id == payment.ProformaId);
            var proforma = await AsyncExecuter.FirstAsync(query);
            if (proforma.Operation.OperationTypeId == EntityEnum.OperationTypeEnum.Manual.GetHashCode())//Manuel operation
            {
                entity.HospitalId = proforma.Operation.HospitalId.Value;
            }
            else
            {
                entity.HospitalId = proforma.Operation.HospitalResponse.HospitalConsultation.HospitalId;
            }
            entity.ProformaNumber = proforma.ProformaCode;
        }
        else
        {
            entity.ProformaNumber = LocalizableString.Create<HTSResource>("NoProforma").Name;
        }

        //Set patient information
        var ptp = await (await _ptpRepository.WithDetailsAsync(p => p.Patient))
           .FirstOrDefaultAsync(p => p.Id == payment.PtpId);
        entity.PatientNameSurname = $"{ptp.Patient.Name} {ptp.Patient.Surname}";
        //Set linenumber
        entity = await SetLineNumber(entity);
        //set item exchangerate TL set 1
        await SetPaymentItems(entity);
        await _paymentRepository.InsertAsync(entity);
    }

    /// <summary>
    /// Set exchangerate clm of paymentitems
    /// </summary>
    /// <param name="payment"></param>
    /// <exception cref="HTSBusinessException"></exception>
    private async Task SetPaymentItems(Payment payment)
    {
        if (payment.PaymentItems?.Any() ?? false)
        {
            //Paymentitem currencies
            List<int> currencies = payment.PaymentItems.Select(i => i.CurrencyId)
                .Distinct()
                .ToList();
            //Get exchangerate information of currencies
            var exchangeRates = await _erRepository.GetListAsync(er => currencies.Contains(er.CurrencyId)
                                             && er.CreationTime.Date.Date == payment.PaymentDate.Date);
            ExchangeRateInformation processingER;
            foreach (var paymentItem in payment.PaymentItems)
            {
                if (paymentItem.CurrencyId == EntityEnum.CurrencyEnum.TL.GetHashCode())//If item is TL set it to 1
                {
                    paymentItem.ExchangeRate = 1;
                }
                else
                {
                    processingER = exchangeRates.FirstOrDefault(e => e.CurrencyId == paymentItem.CurrencyId);
                    if (processingER == null) //No exchange rate
                    {
                        throw new HTSBusinessException(ErrorCode.NoExchangeRateInformation);
                    }
                    paymentItem.ExchangeRate = processingER.ExchangeRate;
                }
            }
        }
    }

    /// <summary>
    /// Generate row number, set rownumber and generatedrownumber clms
    /// </summary>
    /// <param name="entity">payment entity</param>
    /// <returns></returns>
    private async Task<Payment> SetLineNumber(Payment entity)
    {
        var hospital = await _hospitalRepository.FirstOrDefaultAsync(h => h.Id == entity.HospitalId);
        var query = await _paymentRepository.GetQueryableAsync();
        int rowNumber = query.Where(p => p.HospitalId == entity.HospitalId)
            .DefaultIfEmpty()
            .Max(p => p == null ? 0 : p.RowNumber);
        entity.GeneratedRowNumber = $"{hospital.Code}-{++rowNumber}";
        entity.RowNumber = ++rowNumber;
        return entity;
    }

    /// <summary>
    /// Checks if data is valid to create payment
    /// </summary>
    /// <param name="payment">To be saved payment</param>
    /// <exception cref="HTSBusinessException"></exception>
    private async Task IsDataValidToCreate(SavePaymentDto payment)
    {
        if (payment.ProformaId.HasValue)//Proforma related
        {
            if (!await _proformaRepository.AnyAsync(p => p.Id == payment.ProformaId.Value))//No proforma in db
            {
                throw new HTSBusinessException(ErrorCode.RelationalDataIsMissing);
            }
        }

        if (string.IsNullOrEmpty(payment.PayerNameSurname))
        {
            throw new HTSBusinessException(ErrorCode.RequiredFieldsMissing);
        }
    }

}