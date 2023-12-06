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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Json;
using Volo.Abp.Users;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace HTS.Service;
public class USSService : ApplicationService, IUSSService
{
    private readonly IRepository<ENabizProcess, int> _eNabizProcessRepository;
    private readonly IRepository<Process, int> _processRepository;
    private readonly IRepository<Proforma, int> _proformaRepository;
    private readonly IAuditingManager _auditingManager;
    private readonly ICurrentUser _currentUser;
    private IConfiguration _config;

    public USSService(IRepository<ENabizProcess, int> eNabizProcessRepository,
        IRepository<Process, int> processRepository,
        IRepository<Proforma, int> proformaRepository,
        IIdentityUserRepository userRepository,
        IAuditingManager auditingManager,
         IConfiguration config,
        ICurrentUser currentUser)
    {
        _eNabizProcessRepository = eNabizProcessRepository;
        _processRepository = processRepository;
        _proformaRepository = proformaRepository;
        _auditingManager = auditingManager;
        _currentUser = currentUser;
        _config = config;

    }


    public async Task<ExternalApiResult> GetSysTrackingNumber(string treatmentCode)
    {
        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.Add("KullaniciAdi", _config["USSService:KullaniciAdi"]);
        client.DefaultRequestHeaders.Add("Sifre", _config["USSService:Sifre"]);
        client.DefaultRequestHeaders.Add("UygulamaKodu", _config["USSService:UygulamaKodu"]);
       
        HttpResponseMessage messge = await client.GetAsync(_config["USSService:GetSysTrackingNumberServisURL"] + treatmentCode);

        using (var auditingScope = _auditingManager.BeginScope())
        {
            _auditingManager.Current.Log.ApplicationName = "HTS.HttpApi.Host";
            _auditingManager.Current.Log.ExecutionTime = DateTime.Now;
            _auditingManager.Current.Log.ClientId = "HTS_App";
            _auditingManager.Current.Log.HttpMethod = "GET";
            _auditingManager.Current.Log.Url = "USSService";
            _auditingManager.Current.Log.HttpStatusCode = (int)messge.StatusCode;
            _auditingManager.Current.Log.UserName = _currentUser.UserName;
            _auditingManager.Current.Log.UserId = _currentUser.Id;


            AuditLogActionInfo logAction = new AuditLogActionInfo();

            logAction.Parameters = "treatmentCode= " + treatmentCode + "##" + await messge.Content.ReadAsStringAsync();
            logAction.MethodName = "HtsKoduSorgula";
            logAction.ServiceName = "USSService";
            logAction.ExecutionTime = DateTime.Now;
            _auditingManager.Current.Log.Actions.Add(logAction);

            await auditingScope.SaveAsync();

        }

        return System.Text.Json.JsonSerializer.Deserialize<ExternalApiResult>(await messge.Content.ReadAsStringAsync());
    }


    public async Task<ExternalApiResult> GetSysTrackingNumberDetail(string sysTrackingNumber, string treatmentCode)
    {
        ExternalApiResult apiResult = new ExternalApiResult();
        try
        {

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("KullaniciAdi", _config["USSService:KullaniciAdi"]);
            client.DefaultRequestHeaders.Add("Sifre", _config["USSService:Sifre"]);
            client.DefaultRequestHeaders.Add("UygulamaKodu", _config["USSService:UygulamaKodu"]);

            HttpResponseMessage messge = await client.GetAsync(string.Format(_config["USSService:GetSysTrackingNumberDetailURL"] ,sysTrackingNumber ,treatmentCode));
            var serviceResponse = await messge.Content.ReadAsStringAsync();
            apiResult = JsonConvert.DeserializeObject<ExternalApiResult>(serviceResponse);
            

            using (var auditingScope = _auditingManager.BeginScope())
            {
                _auditingManager.Current.Log.ApplicationName = "HTS.HttpApi.Host";
                _auditingManager.Current.Log.ExecutionTime = DateTime.Now;
                _auditingManager.Current.Log.ClientId = "HTS_App";
                _auditingManager.Current.Log.HttpMethod = "GET";
                _auditingManager.Current.Log.Url = "USSService";
                _auditingManager.Current.Log.HttpStatusCode = apiResult.durum;
                _auditingManager.Current.Log.UserName = _currentUser.UserName;
                _auditingManager.Current.Log.UserId = _currentUser.Id;


                ParseLog(_auditingManager, serviceResponse, sysTrackingNumber, treatmentCode);
               
                await auditingScope.SaveAsync();

            }

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

    private void ParseLog(IAuditingManager auditingScope, string message, string sysTrackingNumber, string treatmentCode)
    {
        int start = 0;
        for (int i = 1; i * 1900 <= message.Length; i++)
        {

            AuditLogActionInfo logAction = new AuditLogActionInfo();

            logAction.Parameters = "sysTrackingNumber=" + sysTrackingNumber + "&treatmentCode= " + treatmentCode + "##" + message.Substring(start, 1900);
            logAction.MethodName = "HtsSysTakipNoDetay"+ i.ToString();
            logAction.ServiceName = "USSService";
            logAction.ExecutionTime = DateTime.Now;
            _auditingManager.Current.Log.Actions.Add(logAction);

            
            start += 1900;
        }
        if (message.Length != start)
        {

            AuditLogActionInfo logAction = new AuditLogActionInfo();

            logAction.Parameters = "sysTrackingNumber=" + sysTrackingNumber + "&treatmentCode= " + treatmentCode + "##" + message.Substring(start, message.Length - start - 1);
            logAction.MethodName = "HtsSysTakipNoDetay_last" ;
            logAction.ServiceName = "USSService";
            logAction.ExecutionTime = DateTime.Now;
            _auditingManager.Current.Log.Actions.Add(logAction);
           
        }
    }

    public async Task SetENabizProcess(string treatmentCode)
    {
        ExternalApiResult trackingNumberResult = await GetSysTrackingNumber(treatmentCode);
      
        using (var auditingScope = _auditingManager.BeginScope())
        {
            _auditingManager.Current.Log.ApplicationName = "HTS.HttpApi.Host";
            _auditingManager.Current.Log.ExecutionTime = DateTime.Now;
            _auditingManager.Current.Log.ClientId = "HTS_App";
            _auditingManager.Current.Log.HttpMethod = "GET";
            _auditingManager.Current.Log.Url =  "HtsKoduSorgula" ;
            _auditingManager.Current.Log.HttpStatusCode= 200;
            _auditingManager.Current.Log.UserName = "Enabiz";

            AuditLogActionInfo logAction = new AuditLogActionInfo();
            logAction.Parameters = "treatmentCode= "+ treatmentCode + "##" +  System.Text.Json.JsonSerializer.Serialize(trackingNumberResult.sonuc);
            logAction.ServiceName = "HtsKoduSorgula";
            logAction.ExecutionTime= DateTime.Now;
            _auditingManager.Current.Log.Actions.Add(logAction);
                await auditingScope.SaveAsync();
          
        }


        if (trackingNumberResult != null && trackingNumberResult.sonuc != null && trackingNumberResult.durum == 1)
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
        //Get enabiz items from db
        var eNabizProcesses = await (await _eNabizProcessRepository.GetQueryableAsync())
            .AsNoTracking()
            .Include(p => p.Process)
            .ThenInclude(p => p.ProcessCosts)
            .Where(p => p.TreatmentCode == treatmentCode)
            .ToListAsync();

        //Get proformas of treatmentcode
        var proformas = (await _proformaRepository.WithDetailsAsync())
                               .AsNoTracking()
                               .Where(p => p.Operation.PatientTreatmentProcess.TreatmentCode == treatmentCode
                                           && !notApplicableStatuses.Contains(p.ProformaStatusId))
                                           .ToList();

        //Group to get latest proforma
        var groupList = from p in proformas
                        group p by p.OperationId into g
                        select new
                        {
                            Version = proformas.Where(p => p.OperationId == g.Key).Max(p => p.Version),
                            OperationId = g.Key,
                        };
        proformas = proformas.Where(p => groupList.Any(pp => pp.OperationId == p.OperationId && pp.Version == p.Version)).ToList();
        var proformaProcesses = proformas.SelectMany(p => p.ProformaProcesses).ToList();

        //Calculate proforma process counts
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
                DateTime today = DateTime.Now.Date;
                decimal? ushasUnitPrice = 0, hospitalUnitPrice = 0;
                if (eNabizProcess.Process.ProcessCosts.Any(c => c.ValidityStartDate.Date <= today
                                                   && c.ValidityEndDate >= today))
                {
                    ushasUnitPrice = eNabizProcess.Process.ProcessCosts
                        .FirstOrDefault(c => c.ValidityStartDate.Date <= today && c.ValidityEndDate >= today)?
                        .UshasPrice;
                    hospitalUnitPrice = eNabizProcess.Process.ProcessCosts
                        .FirstOrDefault(c => c.ValidityStartDate.Date <= today && c.ValidityEndDate >= today)?
                        .HospitalPrice;
                }

                //Get proforma process
                var proformaProcess = proformaProcesses.FirstOrDefault(p => p.ProcessId == listENabizProcess.ProcessId);
                if (proformaProcess != null)
                {

                    int proformaCount = processCountLookUp[listENabizProcess.ProcessId.Value];//Count left in proforma
                    int eNabizCount = Convert.ToInt32(listENabizProcess.ADET);//Count come from enabiz
                    if (eNabizCount <= proformaCount)
                    {//There is enough count. Mark as usedinproforma
                        listENabizProcess.IsUsedInProforma = true;
                        listENabizProcess.UshasPrice = proformaProcess.UnitPrice * eNabizCount;
                        listENabizProcess.HospitalPrice = hospitalUnitPrice * eNabizCount;
                        processCountLookUp[listENabizProcess.ProcessId.Value] -= eNabizCount;
                    }
                    else if (proformaCount > 0)
                    {
                        listENabizProcess.ADET = proformaCount.ToString();
                        processCountLookUp[listENabizProcess.ProcessId.Value] = 0;
                        listENabizProcess.IsUsedInProforma = true;
                        listENabizProcess.UshasPrice = proformaProcess.UnitPrice * proformaCount;
                        listENabizProcess.HospitalPrice = hospitalUnitPrice * proformaCount;
                        responseList.Add(listENabizProcess);
                        listENabizProcess = ObjectMapper.Map<ENabizProcess, ListENabizProcessDto>(eNabizProcess);
                        listENabizProcess.IsUsedInProforma = false;
                        listENabizProcess.ADET = (eNabizCount - proformaCount).ToString();
                        listENabizProcess.UshasPrice = proformaProcess.UnitPrice * (eNabizCount - proformaCount);
                        listENabizProcess.HospitalPrice = hospitalUnitPrice * (eNabizCount - proformaCount);
                    }

                    else if (proformaCount == 0)
                    {
                        listENabizProcess.UshasPrice = proformaProcess.UnitPrice * (eNabizCount - proformaCount);
                        listENabizProcess.HospitalPrice = hospitalUnitPrice * (eNabizCount - proformaCount);
                    }
                }
                else
                {//processId is not in proforma
                    listENabizProcess.UshasPrice = ushasUnitPrice * Convert.ToInt32(listENabizProcess.ADET);
                    listENabizProcess.HospitalPrice = hospitalUnitPrice * Convert.ToInt32(listENabizProcess.ADET);
                }
            }
            responseList.Add(listENabizProcess);
        }
        return responseList;
    }
}