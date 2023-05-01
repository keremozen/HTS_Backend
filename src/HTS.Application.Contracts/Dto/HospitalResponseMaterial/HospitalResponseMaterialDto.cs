using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.HospitalResponseMaterial
{
    public class HospitalResponseMaterialDto : EntityDto<int>
    {
        public int HospitalResponseId { get; set; }
        public int MaterialId { get; set; }
    }
}