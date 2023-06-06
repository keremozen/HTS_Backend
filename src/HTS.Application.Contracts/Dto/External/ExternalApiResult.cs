using System.Collections.Generic;

namespace HTS.Dto.External;

public class ExternalApiResult
{
    public int ResultCode { get; set; }
    public List<SutCodeResult> Result { get; set; }
}

public class SutCodeResult
{
    public string SutCode { get; set; }
    public bool IsIncluded { get; set; }
}