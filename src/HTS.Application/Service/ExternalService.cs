using System;
using System.Collections.Generic;
using System.Linq;
using HTS.Data.Entity;
using HTS.Dto.City;
using HTS.Interface;
using System.Threading.Tasks;
using HTS.Dto;
using HTS.Dto.External;
using HTS.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Auditing;
using Volo.Abp.Users;

namespace HTS.Service
{

    public class ExternalService : ApplicationService, IExternalService
    {

        private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;
        private readonly IRepository<Proforma, int> _proformaRepository;
        private readonly IRepository<Process, int> _processRepository;
        private readonly IAuditingManager _auditingManager;
     

        public ExternalService(IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository,
        IRepository<Proforma, int> proformaRepository,
         IAuditingManager auditingManager,
        IRepository<Process, int> processRepository)
        {
            _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
            _proformaRepository = proformaRepository;
            _processRepository = processRepository;
            _auditingManager = auditingManager;
        }


        public async Task<ExternalApiResult> HtsHizmetKoduKontrol(SutCodesRequestDto sutCodesRequest)
        {
            ExternalApiResult result;
            var proforma = await (await _proformaRepository.WithDetailsAsync(p => p.ProformaProcesses))
                .Where(p => p.Operation.PatientTreatmentProcess.TreatmentCode == sutCodesRequest.HtsKodu
                            && p.ProformaStatusId == EntityEnum.ProformaStatusEnum.PaymentCompleted.GetHashCode())
                .FirstOrDefaultAsync();
            if (proforma != null)
            {
                List<int> proformaProcessIds = proforma.ProformaProcesses.Select(p => p.ProcessId).ToList();
                var processes = await _processRepository.GetListAsync(p => proformaProcessIds.Contains(p.Id));
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new List<SutCodeResult>(),
                    mesaj = null
                };
                bool containsProcess;
                foreach (var sutCode in sutCodesRequest.SutKoduList)
                {
                    containsProcess = processes.Any(p => p.Code == sutCode);
                    ((List<SutCodeResult>)result.sonuc).Add(new SutCodeResult()
                    {
                        gecerliMi = containsProcess,
                        islemTarihi = containsProcess ? proforma.CreationDate : DateTime.MinValue,
                        klinikKodu = containsProcess ? "123456" : null,
                        sutKodu = sutCode
                    });
                }
            }
            else//Generate test response
            {
                result = GenerateTestData_HtsHizmetKoduKontrol(sutCodesRequest);
            }

            using (var auditingScope = _auditingManager.BeginScope())
            {
                _auditingManager.Current.Log.ApplicationName = "HTS.HttpApi.Host";
                _auditingManager.Current.Log.ExecutionTime = DateTime.Now;
                _auditingManager.Current.Log.ClientId = "HTS_App";
                _auditingManager.Current.Log.HttpMethod = "GET";
                _auditingManager.Current.Log.Url = "ExternalService";
                _auditingManager.Current.Log.HttpStatusCode = (int)result.durum;
                _auditingManager.Current.Log.UserName = "enabiz";



                AuditLogActionInfo logAction = new AuditLogActionInfo();
                logAction.ExtraProperties.Add("Request", Newtonsoft.Json.JsonConvert.SerializeObject(sutCodesRequest));
                logAction.Parameters = "treatmentCode= " + sutCodesRequest.HtsKodu + "##" + Newtonsoft.Json.JsonConvert.SerializeObject( result.sonuc);
                logAction.MethodName = "HtsHizmetKoduKontrol";
                logAction.ServiceName = "ExternalService";
                logAction.ExecutionTime = DateTime.Now;
                _auditingManager.Current.Log.Actions.Add(logAction);

                await auditingScope.SaveAsync();

            }
            return result;
        }

        public async Task<ExternalApiResult> HtsHastaBilgisi(string htsCode)
        {
            ExternalApiResult result;
            var patientTreatmentProcess = await (await _patientTreatmentProcessRepository.WithDetailsAsync((ptp => ptp.Patient),
                    (ptp => ptp.Patient.Nationality)))
                 .Where(ptp => ptp.TreatmentCode == htsCode).FirstOrDefaultAsync();
            if (patientTreatmentProcess != null)
            {
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new PatientInfo()
                    {
                        cinsiyet = patientTreatmentProcess.Patient.GenderId?.ToString() ?? string.Empty,
                        adi = patientTreatmentProcess.Patient.Name,
                        soyadi = patientTreatmentProcess.Patient.Surname,
                        ulkeKodu = patientTreatmentProcess.Patient.Nationality.CountryCode,
                        pasaport = patientTreatmentProcess.Patient.PassportNumber
                    },
                    mesaj = null
                };
            }
            else
            {
                result = GenerateTestData_HtsHastaBilgisi(htsCode);
            }

            using (var auditingScope = _auditingManager.BeginScope())
            {
                _auditingManager.Current.Log.ApplicationName = "HTS.HttpApi.Host";
                _auditingManager.Current.Log.ExecutionTime = DateTime.Now;
                _auditingManager.Current.Log.ClientId = "HTS_App";
                _auditingManager.Current.Log.HttpMethod = "GET";
                _auditingManager.Current.Log.Url = "ExternalService";
                _auditingManager.Current.Log.HttpStatusCode = (int)result.durum;
                _auditingManager.Current.Log.UserName = "enabiz";
              


                AuditLogActionInfo logAction = new AuditLogActionInfo();

                logAction.Parameters = "treatmentCode= " + htsCode + "##" + Newtonsoft.Json.JsonConvert.SerializeObject(result.sonuc);
                logAction.MethodName = "HtsHastaBilgisi";
                logAction.ServiceName = "ExternalService";
                logAction.ExecutionTime = DateTime.Now;
                _auditingManager.Current.Log.Actions.Add(logAction);

                await auditingScope.SaveAsync();

            }

            return result;

        }

        private ExternalApiResult GenerateTestData_HtsHastaBilgisi(string htsCode)
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
            else if (htsCode == "U570126260")
            {
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new PatientInfo()
                    {
                        cinsiyet = "1",
                        adi = "Ahmet",
                        soyadi = "Mehmet",
                        ulkeKodu = "TR",
                        pasaport = ""
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

        private ExternalApiResult GenerateTestData_HtsHizmetKoduKontrol(SutCodesRequestDto sutCodesRequest)
        {
            ExternalApiResult result;
            if (sutCodesRequest.HtsKodu == "U100000000")
            {
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new List<SutCodeResult>(),
                    mesaj = null
                };
                foreach (var sutCode in sutCodesRequest.SutKoduList)
                {
                    ((List<SutCodeResult>)result.sonuc).Add(new SutCodeResult()
                    {
                        gecerliMi = (sutCode == "GR1000" || sutCode == "GR1001") ? true : false
                    ,
                        sutKodu = sutCode
                    });
                }
            }
            else if (sutCodesRequest.HtsKodu == "U100000001")
            {
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new List<SutCodeResult>(),
                    mesaj = null
                };
                foreach (var sutCode in sutCodesRequest.SutKoduList)
                {
                    ((List<SutCodeResult>)result.sonuc).Add(new SutCodeResult()
                    {
                        gecerliMi = (sutCode == "GR2000" || sutCode == "GR2001") ? true : false
                    ,
                        sutKodu = sutCode
                    });
                }
            }
            else if (sutCodesRequest.HtsKodu == "U570126260")
            {
                result = new ExternalApiResult()
                {
                    durum = 200,
                    sonuc = new List<SutCodeResult>(),
                    mesaj = null
                };
                foreach (var sutCode in sutCodesRequest.SutKoduList)
                {
                    ((List<SutCodeResult>)result.sonuc).Add(new SutCodeResult()
                    {
                        gecerliMi = true,
                        islemTarihi = new DateTime(2023, 2, 23),
                        klinikKodu = "123456",
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

    }
}
