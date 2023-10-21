using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.External;
using HTS.Dto.Nationality;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;
public class USSService : IUSSService
{
    public USSService() 
    {
        
    }
    

    public async Task<ExternalApiResult> GetSysTrackingNumber(string treatmentCode)
    {
        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.Add("KullaniciAdi", "10000053");
        client.DefaultRequestHeaders.Add("Sifre", "Htstest2023.");
        client.DefaultRequestHeaders.Add("UygulamaKodu", "f688ab0e-f6f9-42ee-9c86-0c623ed02351");
        
        HttpResponseMessage messge = await client.GetAsync("https://ussservistest.saglik.gov.tr/api/Hts/HtsKoduSorgula?htsKodu=" + treatmentCode);
        return JsonSerializer.Deserialize<ExternalApiResult>(await  messge.Content.ReadAsStringAsync());     
    }
}