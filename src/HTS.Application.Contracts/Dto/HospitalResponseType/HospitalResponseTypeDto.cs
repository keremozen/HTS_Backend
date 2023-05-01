using Volo.Abp.Application.Dtos;

namespace HTS.Dto.HospitalResponseType
{
    public class HospitalResponseTypeDto : EntityDto<int>
    {
        public string Name { get; set; }
    }
}
