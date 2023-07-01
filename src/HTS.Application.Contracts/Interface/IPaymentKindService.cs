using HTS.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.Gender;
using HTS.Dto.PaymentKind;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface IPaymentKindService : IApplicationService
    {
        Task<ListResultDto<PaymentKindDto>> GetListAsync();
    }
}
