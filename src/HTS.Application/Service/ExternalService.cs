using System.Collections.Generic;
using System.Linq;
using HTS.Data.Entity;
using HTS.Dto.City;
using HTS.Interface;
using System.Threading.Tasks;
using HTS.Dto;
using HTS.Dto.External;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    [Authorize]
    public class ExternalService : ApplicationService, IExternalService
    {

        public async Task<ExternalApiResult> CheckSutCodes(SutCodesRequestDto sutCodesRequest)
        {
            ExternalApiResult result;
            if (sutCodesRequest.HTSCode == "U100000000")
            {
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new List<SutCodeResult>(),
                    mesaj = null
                };
                foreach (var sutCode in sutCodesRequest.SutCodes)
                {
                    ((List<SutCodeResult>)result.sonuc).Add(new SutCodeResult()
                    {
                        gecerliMi = (sutCode == "GR1000" || sutCode == "GR1001") ? true : false
                    ,
                        sutKodu = sutCode
                    });
                }
            }
            else if (sutCodesRequest.HTSCode == "U100000001")
            {
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new List<SutCodeResult>(),
                    mesaj = null
                };
                foreach (var sutCode in sutCodesRequest.SutCodes)
                {
                    ((List<SutCodeResult>)result.sonuc).Add(new SutCodeResult()
                    {
                        gecerliMi = (sutCode == "GR2000" || sutCode == "GR2001") ? true : false
                    ,
                        sutKodu = sutCode
                    });
                }
            }
            else
            {
                result = new ExternalApiResult()
                {
                    durum = 201,
                    sonuc = null,
                    mesaj = null
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
                    durum = 200,
                    sonuc = new PatientInfo()
                    {
                        cinsiyet = "1",
                        adi = "Yeager",
                        soyadi = "Jacobsen",
                        ulkeKodu = "DE",
                        pasaport = "UP1234EY"
                    },
                    mesaj = null
                };
            }
            else if (htsCode == "U100000001")
            {
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new PatientInfo()
                    {
                        cinsiyet = "2",
                        adi = "Aerır",
                        soyadi = "Jacobsen",
                        ulkeKodu = "DE",
                        pasaport = "UP1234EZ"
                    },
                    mesaj = null
                };
            }
            else
            {
                result = new ExternalApiResult()
                {
                    durum = 201,
                    sonuc = null,
                    mesaj = null
                };
            }
            return result;

        }
    }
}
