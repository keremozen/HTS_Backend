using System.Collections.Generic;
using System.Linq;
using HTS.Data.Entity;
using HTS.Dto.City;
using HTS.Interface;
using System.Threading.Tasks;
using HTS.Dto;
using HTS.Dto.External;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    public class ExternalService : ApplicationService, IExternalService
    {

        public async Task<ExternalApiResult> CheckSutCodes(SutCodesRequestDto sutCodesRequest)
        {
            ExternalApiResult result = new ExternalApiResult()
            {
                ResultCode = 200,
                Result = new List<SutCodeResult>()
            };
            int i = 0;
            foreach (var sutCode in sutCodesRequest.SutCodes)
            {
                result.Result.Add(new SutCodeResult(){ IsIncluded = (++i % 2 == 0), SutCode = sutCode});
            }
            return result;
        }
    }
}
