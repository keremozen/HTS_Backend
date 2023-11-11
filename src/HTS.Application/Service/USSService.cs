using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using HTS.Data.Entity;
using HTS.Dto.External;
using HTS.Dto.Nationality;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Json;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace HTS.Service;
public class USSService : ApplicationService, IUSSService
{
    private readonly IRepository<ENabizProcess, int> _eNabizProcessRepository;
    public USSService(IRepository<ENabizProcess, int> eNabizProcessRepository,
        IIdentityUserRepository userRepository)
    {
        _eNabizProcessRepository = eNabizProcessRepository;
    }


    public async Task<ExternalApiResult> GetSysTrackingNumber(string treatmentCode)
    {
        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.Add("KullaniciAdi", "10000053");
        client.DefaultRequestHeaders.Add("Sifre", "Htstest2023.");
        client.DefaultRequestHeaders.Add("UygulamaKodu", "f688ab0e-f6f9-42ee-9c86-0c623ed02351");

        HttpResponseMessage messge = await client.GetAsync("https://ussservistest.saglik.gov.tr/api/Hts/HtsKoduSorgula?htsKodu=" + treatmentCode);
        return System.Text.Json.JsonSerializer.Deserialize<ExternalApiResult>(await messge.Content.ReadAsStringAsync());
    }


    public async Task<ExternalApiResult> GetSysTrackingNumberDetail(string sysTrackingNumber, string treatmentCode)
    {
        ExternalApiResult apiResult = new ExternalApiResult();
        try
        {
           
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("KullaniciAdi", "10000053");
            client.DefaultRequestHeaders.Add("Sifre", "Htstest2023.");
            client.DefaultRequestHeaders.Add("UygulamaKodu", "f688ab0e-f6f9-42ee-9c86-0c623ed02351");

            HttpResponseMessage messge = await client.GetAsync("https://ussservistest.saglik.gov.tr/api/Hts/HtsSysTakipNoDetay?sysTakipNo=" + sysTrackingNumber + "&htsKodu=" + treatmentCode);
            var serviceResponse = await messge.Content.ReadAsStringAsync();
            apiResult = JsonConvert.DeserializeObject<ExternalApiResult>(serviceResponse);
           
            if (apiResult.durum != 1)
            {
                return apiResult;
            }
            var root = JObject.Parse(serviceResponse);
            var att = root["sonuc"]["recordData"]?["HASTA_ISLEM_BILGILERI"]?["ISLEM_BILGISI"] ?? null;
            if (att == null)
            {
                return apiResult;
            }

            IDictionary<string, string> dic = new Dictionary<string, string>();
            foreach (JProperty attributeProperty in att)
            {
                var attribute = att[attributeProperty.Name];
                if (attribute != null && attribute["@value"] != null)
                {
                    var my_data = attribute["@value"].Value<string>();
                    dic.Add(attributeProperty.Name, my_data);
                }
            }
            var json = JsonConvert.SerializeObject(dic, Newtonsoft.Json.Formatting.Indented);
            ENabizProcessDto islemBilgisi = JsonConvert.DeserializeObject<ENabizProcessDto>(json);
            var entity = ObjectMapper.Map<ENabizProcessDto, ENabizProcess>(islemBilgisi);
            entity.TreatmentCode = treatmentCode;
            entity.SysTrackingNumber = sysTrackingNumber;
            await _eNabizProcessRepository.InsertAsync(entity);

        }
        catch (Exception ex)
        {
            apiResult.mesaj = ex.Message;
            apiResult.durum = 0;

        }
        return apiResult;
    }


    public async Task SetENabizProcess(string treatmentCode)
    {
        ExternalApiResult trackingNumberResult = await GetSysTrackingNumber(treatmentCode);
        if (trackingNumberResult != null && trackingNumberResult.durum == 1)
        {
            List<HTSCodeResult> sysCodes = System.Text.Json.JsonSerializer.Deserialize<List<HTSCodeResult>>((JsonElement)trackingNumberResult.sonuc);
            foreach (var sysCode in sysCodes)
            {
                await GetSysTrackingNumberDetail(sysCode.sysTakipNo, treatmentCode);
            }
        }
    }
}