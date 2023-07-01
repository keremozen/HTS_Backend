using HTS.Data.Entity;
using HTS.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Dto.Gender;
using HTS.Dto.PaymentKind;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    [Authorize]
    public class PaymentKindService : ApplicationService, IPaymentKindService
    {

        private readonly IRepository<PaymentKind, int> _paymentKindRepository;
        public PaymentKindService(IRepository<PaymentKind, int> paymentKindRepository)
        {
            _paymentKindRepository = paymentKindRepository;

        }
        public async Task<ListResultDto<PaymentKindDto>> GetListAsync()
        {
            var responseList = ObjectMapper.Map<List<PaymentKind>, List<PaymentKindDto>>(await _paymentKindRepository.GetListAsync());
            //Return the result
            return new ListResultDto<PaymentKindDto>(responseList);
        }
    }
}
