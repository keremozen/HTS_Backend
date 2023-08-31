using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Nationality;
using HTS.Dto.PaymentReason;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
[Authorize("HTS.PaymentReasonManagement")]
public class PaymentReasonService : ApplicationService, IPaymentReasonService
{
    private readonly IRepository<PaymentReason, int> _prRepository;
    public PaymentReasonService(IRepository<PaymentReason, int> prRepository) 
    {
        _prRepository = prRepository;
    }
    
    public async Task<PaymentReasonDto> GetAsync(int id)
    {
        return ObjectMapper.Map<PaymentReason, PaymentReasonDto>(await _prRepository.GetAsync(id));
    }

    public async Task<ListResultDto<PaymentReasonDto>> GetListAsync(bool? isActive=null)
    {
        var query = await _prRepository.GetQueryableAsync();
        query = query.WhereIf(isActive.HasValue,
            n => n.IsActive == isActive.Value);
        var responseList = ObjectMapper.Map<List<PaymentReason>, List<PaymentReasonDto>>(await AsyncExecuter.ToListAsync(query));
        return new ListResultDto<PaymentReasonDto>(responseList);
    }

    public async Task CreateAsync(SavePaymentReasonDto paymentReason)
    {
        var entity = ObjectMapper.Map<SavePaymentReasonDto, PaymentReason>(paymentReason);
        await _prRepository.InsertAsync(entity);
    }

    public async Task UpdateAsync(int id, SavePaymentReasonDto paymentReason)
    {
        var entity = await _prRepository.GetAsync(id);
        ObjectMapper.Map(paymentReason, entity);
        await _prRepository.UpdateAsync(entity);
    }
        
    public async Task DeleteAsync(int id)
    {
        await _prRepository.DeleteAsync(id);
    }
}