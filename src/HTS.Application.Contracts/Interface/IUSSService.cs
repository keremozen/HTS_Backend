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
    public interface IUSSService : IApplicationService
    {
        Task<ExternalApiResult> GetSysTrackingNumber(string treatmentCode);
        Task<ExternalApiResult> GetSysTrackingNumberDetail(string sysTrackingNumber, string treatmentCode);
        Task<ExternalApiResult> SetENabizProcess(string treatmentCode);
    }
}
