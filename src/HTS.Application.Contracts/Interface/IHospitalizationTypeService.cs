using System.Threading.Tasks;
using HTS.Dto.HospitalizationType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface;

public interface IHospitalizationTypeService : IApplicationService
{
    /// <summary>
    /// Get all hospitalization types 
    /// </summary>
    /// <returns>Hospitalization type list</returns>
    Task<PagedResultDto<HospitalizationTypeDto>> GetListAsync();
 
}