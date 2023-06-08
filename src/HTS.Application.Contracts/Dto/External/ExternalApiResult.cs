using System.Collections.Generic;

namespace HTS.Dto.External;

public class ExternalApiResult
{
    public int ResultCode { get; set; }
    public object Result { get; set; }
}

public class SutCodeResult
{
    public string SutCode { get; set; }
    public bool IsIncluded { get; set; }
}

public class PatientInfo
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Nationality { get; set; }
    public string Passport { get; set; }
    public string Gender { get; set; }



}