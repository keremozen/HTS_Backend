using System;
using System.Collections.Generic;

namespace HTS.Dto.External;

public class ExternalApiResult
{
    public int durum { get; set; }
    public object sonuc { get; set; }
    public string mesaj { get; set; }
}

public class SutCodeResult
{
    public string sutKodu { get; set; }
    public bool gecerliMi { get; set; }
    public string klinikKodu { get; set; }
    public DateTime islemTarihi { get; set; }
}

public class PatientInfo
{
    public string adi { get; set; }
    public string soyadi { get; set; }
    public string ulkeKodu { get; set; }
    public string pasaport { get; set; }
    public string cinsiyet { get; set; }

}

public class GetSysTrackingNumberObject
{
    public string sysTakipNo { get; set; }
    public string uyruk { get; set; }
    public string pasaportNo { get; set; }
    public string kurumKodu { get; set; }
}

public class ENabizProcessDto
{
    public string GERCEKLESME_ZAMANI { get; set; }
    public string ISLEM_TURU { get; set; }
    public string ISLEM_KODU { get; set; }
    public string ISLEM_ADI { get; set; }
    public string ISLEM_ZAMANI { get; set; }
    public string ADET { get; set; }
    public string HASTA_TUTARI { get; set; }
    public string KURUM_TUTARI { get; set; }
    public string RANDEVU_ZAMANI { get; set; }
    public string KULLANICI_KIMLIK_NUMARASI { get; set; }
    public string CIHAZ_NUMARASI { get; set; }
    public string ISLEM_REFERANS_NUMARASI { get; set; }
    public string GIRISIMSEL_ISLEM_KODU { get; set; }
    public string KLINIK_KODU { get; set; }
    public string ISLEM_PUAN_BILGISI { get; set; }
}

public class ListENabizProcessDto : ENabizProcessDto
{
    public string TreatmentCode { get; set; }
    public string SysTrackingNumber { get; set; }
    public int? ProcessId { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsUsedInProforma { get; set; }
}