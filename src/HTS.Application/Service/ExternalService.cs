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
            ExternalApiResult result;
            if (sutCodesRequest.HTSCode == "U100000000")
            {
                result = new ExternalApiResult()
                {
                    ResultCode = 200,
                    Result = new List<SutCodeResult>()
                };
                foreach (var sutCode in sutCodesRequest.SutCodes)
                {
                    ((List<SutCodeResult>)result.Result).Add(new SutCodeResult()
                    {
                        IsIncluded = (sutCode == "GR1000" || sutCode == "GR1001") ? true : false
                    ,
                        SutCode = sutCode
                    });
                }
            }
            else if (sutCodesRequest.HTSCode == "U100000001")
            {
                result = new ExternalApiResult()
                {
                    ResultCode = 200,
                    Result = new List<SutCodeResult>()
                };
                foreach (var sutCode in sutCodesRequest.SutCodes)
                {
                    ((List<SutCodeResult>)result.Result).Add(new SutCodeResult()
                    {
                        IsIncluded = (sutCode == "GR2000" || sutCode == "GR2001") ? true : false
                    ,
                        SutCode = sutCode
                    });
                }
            }
            else
            {
                result = new ExternalApiResult()
                {
                    ResultCode = 201,
                    Result = null
                };
            }
            return result;
        }

        public async Task<ExternalApiResult> GetPatientInfo(string htsCode)
        {
            ExternalApiResult result;
            if (htsCode == "U100000000")
            {
                result = new ExternalApiResult()
                {
                    ResultCode = 200,
                    Result = new PatientInfo()
                    {
                        Gender = "1",
                        Name = "Yeager",
                        Surname = "Jacobsen",
                        Nationality = "DE",
                        Passport = "UP1234EY"
                    }
                };
            }
            else if (htsCode == "U100000001")
            {
                result = new ExternalApiResult()
                {
                    ResultCode = 200,
                    Result = new PatientInfo()
                    {
                        Gender = "2",
                        Name = "Aerır",
                        Surname = "Jacobsen",
                        Nationality = "DE",
                        Passport = "UP1234EZ"
                    }
                };
            }
            else
            {
                result = new ExternalApiResult()
                {
                    ResultCode = 201,
                    Result = null
                };
            }
            return result;

        }
    }
}
