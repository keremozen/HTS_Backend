using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.HospitalResponseBranch
{
    public class HospitalResponseBranchDto : EntityDto<int>
    {
        public int HospitalResponseId { get; set; }
        public int BranchId { get; set; }
    }
}