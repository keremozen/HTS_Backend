using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Payment;
using HTS.Dto.Process;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
[Authorize]
public class PaymentService : ApplicationService, IPaymentService
{
    private readonly IRepository<Payment, int> _paymentRepository;
    private readonly IRepository<PaymentItem, int> _paymentItemRepository;
    public PaymentService(IRepository<Payment, int> paymentRepository, 
        IRepository<PaymentItem, int> paymentItemRepository)
    {
        _paymentRepository = paymentRepository;
        _paymentItemRepository = paymentItemRepository;
    }
    

    public async Task<PagedResultDto<PaymentDto>> GetListAsync(int ptpId)
    {
        //Get all entities
        var query = (await _paymentRepository.WithDetailsAsync())
            .Where(p => p.PtpId == ptpId);
        var responseList = ObjectMapper.Map<List<Payment>, List<PaymentDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _paymentRepository.CountAsync();//item count
        return new PagedResultDto<PaymentDto>(totalCount, responseList);
    }

    public async Task CreateAsync(SavePaymentDto payment)
    {
        var entity = ObjectMapper.Map<SavePaymentDto, Payment>(payment);
        await _paymentRepository.InsertAsync(entity);
    }
    
}