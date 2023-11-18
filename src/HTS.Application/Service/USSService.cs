using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using HTS.Data.Entity;
using HTS.Dto.External;
using HTS.Dto.Nationality;
using HTS.Enum;
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
    private readonly IRepository<Process, int> _processRepository;
    private readonly IRepository<Proforma, int> _proformaRepository;


    public USSService(IRepository<ENabizProcess, int> eNabizProcessRepository,
        IRepository<Process, int> processRepository,
        IRepository<Proforma, int> proformaRepository,
        IIdentityUserRepository userRepository)
    {
        _eNabizProcessRepository = eNabizProcessRepository;
        _processRepository = processRepository;
        _proformaRepository = proformaRepository;
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
            entity.IsCancelled = false;

            //Check with refnumber.
            if (!await _eNabizProcessRepository.AnyAsync(p => p.ISLEM_REFERANS_NUMARASI == entity.ISLEM_REFERANS_NUMARASI))
            {//New record, insert
             //Get related process 
                var process = await _processRepository.FirstOrDefaultAsync(p => p.Code.Contains(entity.ISLEM_KODU) && p.IsDeleted == false);
                if (process != null)
                {
                    entity.ProcessId = process.Id;
                }
                await _eNabizProcessRepository.InsertAsync(entity);
            }
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
            List<GetSysTrackingNumberObject> sysCodes = System.Text.Json.JsonSerializer.Deserialize<List<GetSysTrackingNumberObject>>((JsonElement)trackingNumberResult.sonuc);
            foreach (var sysCode in sysCodes)
            {
                await GetSysTrackingNumberDetail(sysCode.sysTakipNo, treatmentCode);
            }
        }
    }

    public async Task<List<ListENabizProcessDto>> GetENabizProcesses(string treatmentCode)
    {
        //TODO:Hopsy child processler
        List<int> notApplicableStatuses = new List<int>()
        {
             EntityEnum.ProformaStatusEnum.MFBRejected.GetHashCode(),
              EntityEnum.ProformaStatusEnum.PatientRejected.GetHashCode(),
        };
        var eNabizProcesses = (await _eNabizProcessRepository.WithDetailsAsync(p => p.Process)).Where(p => p.TreatmentCode == treatmentCode).ToList();

        var proformas = (await _proformaRepository.WithDetailsAsync())
                               .Where(p => p.Operation.PatientTreatmentProcess.TreatmentCode == treatmentCode
                                           && !notApplicableStatuses.Contains(p.ProformaStatusId))
                                           .ToList();

        var groupList = from p in proformas
                        group p by p.OperationId into g
                        select new
                        {
                            Version = proformas.Where(p => p.OperationId == g.Key).Max(p => p.Version),
                            OperationId = g.Key,
                        };
        proformas = proformas.Where(p => groupList.Any(pp => pp.OperationId == p.OperationId && pp.Version == p.Version)).ToList();
        var proformaProcesses = proformas.SelectMany(p => p.ProformaProcesses).ToList();


        Dictionary<int, int> processCountLookUp = new Dictionary<int, int>();
        foreach (var pProcess in proformaProcesses)
        {
            if (processCountLookUp.ContainsKey(pProcess.ProcessId))
            {
                processCountLookUp[pProcess.ProcessId] += pProcess.TreatmentCount;
            }
            else
            {
                processCountLookUp.Add(pProcess.ProcessId, pProcess.TreatmentCount);
            }
        }

        List<ListENabizProcessDto> responseList = new List<ListENabizProcessDto>();
        foreach (var eNabizProcess in eNabizProcesses)
        {
            ListENabizProcessDto listENabizProcess = ObjectMapper.Map<ENabizProcess, ListENabizProcessDto>(eNabizProcess);
            listENabizProcess.IsUsedInProforma = false;
            if (!string.IsNullOrEmpty(listENabizProcess.ADET)
                && listENabizProcess.ProcessId != null)
            {
                //Get proforma process
                var proformaProcess = proformaProcesses.FirstOrDefault(p => p.ProcessId == listENabizProcess.ProcessId);
                if (proformaProcess != null)
                {
                    listENabizProcess.UshasPrice = proformaProcess.UnitPrice;
                    int proformaCount = processCountLookUp[listENabizProcess.ProcessId.Value];
                    int eNabizCount = Convert.ToInt32(listENabizProcess.ADET);
                    if (eNabizCount <= proformaCount)
                    {
                        listENabizProcess.IsUsedInProforma = true;
                        processCountLookUp[listENabizProcess.ProcessId.Value] -= eNabizCount;
                    }
                    else if (proformaCount > 0)
                    {
                        listENabizProcess.ADET = proformaCount.ToString();
                        processCountLookUp[listENabizProcess.ProcessId.Value] = 0;
                        listENabizProcess.IsUsedInProforma = true;
                        responseList.Add(listENabizProcess);
                        listENabizProcess = ObjectMapper.Map<ENabizProcess, ListENabizProcessDto>(eNabizProcess);
                        listENabizProcess.IsUsedInProforma = false;
                        listENabizProcess.ADET = (eNabizCount - proformaCount).ToString();
                    }
                }
                else
                {//processId is not in proforma

                    var process = (await _processRepository.WithDetailsAsync(p => p.ProcessCosts))
                        .FirstOrDefault(p => p.Id == listENabizProcess.ProcessId);
                    if (process != null)
                    {
                        DateTime today = DateTime.Now.Date;
                        if (process.ProcessCosts.Any(c => c.ValidityStartDate.Date <= today
                                                           && c.ValidityEndDate >= today))
                        {
                            listENabizProcess.UshasPrice = process.ProcessCosts
                                .FirstOrDefault(c => c.ValidityStartDate.Date <= today && c.ValidityEndDate >= today)?
                                .UshasPrice;
                        }
                    }
                }
            }
            responseList.Add(listENabizProcess);
        }
        return responseList;
    }
}