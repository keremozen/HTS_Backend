using System.Collections.Generic;

namespace HTS.Dto.External;

public class SutCodesRequestDto
{
    public string HTSCode { get; set; }
    public List<string> SutCodes { get; set; }
}