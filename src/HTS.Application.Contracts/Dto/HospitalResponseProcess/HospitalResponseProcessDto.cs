using HTS.Dto.Process;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.HospitalResponseProcess
{
    public class HospitalResponseProcessDto : EntityDto<int>
    {
        public int HospitalResponseId { get; set; }
        public int ProcessId { get; set; }
        public int Amount { get; set; }
        public ProcessDto Process { get; set; }
    }
}