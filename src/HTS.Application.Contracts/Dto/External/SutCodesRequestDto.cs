using System.Collections.Generic;

namespace HTS.Dto.External;

public class SutCodesRequestDto
{
    public string HtsKodu { get; set; }
    public List<string> SutKoduList { get; set; }
    public string SysTakipNo { get; set; }
}