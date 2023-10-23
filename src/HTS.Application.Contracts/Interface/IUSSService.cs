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
    public interface IUSSService :IApplicationService
    {
        /// <summary>
        /// Gets sysTrackingNumber related to treatment code from enabiz system 
        /// </summary>
        /// <param name="treatmentCode">Related treatment code</param>
        /// <returns>ServiceResponse</returns>
        public Task<ExternalApiResult> GetSysTrackingNumber(string treatmentCode);
    }
}
