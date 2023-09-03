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
namespace HTS.Service
{
    [Authorize]
    public class ExternalService : ApplicationService, IExternalService
    {

        private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;
        private readonly IRepository<Proforma, int> _proformaRepository;
        private readonly IRepository<Process, int> _processRepository;

        public ExternalService(IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository,
        IRepository<Proforma, int> proformaRepository,
        IRepository<Process, int> processRepository)
        {
            _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
            _proformaRepository = proformaRepository;
            _processRepository = processRepository;
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
                        sutKodu = "S102040"
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
