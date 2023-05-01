using Volo.Abp.Application.Dtos;

namespace HTS.Dto.HospitalResponseProcess
{
    public class HospitalResponseProcessDto : EntityDto<int>
    {
        public int HospitalResponseId { get; set; }
        public int ProcessId { get; set; }
        public int Count { get; set; }
    }
}