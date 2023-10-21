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

public class HTSCodeResult
{
    public string sysTakipNo { get; set; }
    public string uyruk { get; set; }
    public string pasaportNo { get; set; }
    public string kurumKodu { get; set; }
}