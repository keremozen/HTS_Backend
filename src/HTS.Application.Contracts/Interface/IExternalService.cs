using HTS.Dto;
using HTS.Dto.City;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.External;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface IExternalService : IApplicationService
    {
        Task<ExternalApiResult> CheckSutCodes(SutCodesRequestDto sutCodesRequest);
    }
}
