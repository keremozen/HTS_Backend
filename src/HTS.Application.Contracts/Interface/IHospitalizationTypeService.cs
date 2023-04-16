using System.Threading.Tasks;
using HTS.Dto.HospitalizationType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface;

public interface IHospitalizationTypeService : IApplicationService
{
    /// <summary>
    /// Get hospitalization type by id
    /// </summary>
    /// <param name="id">Desired hospitalization type id</param>
    /// <returns>Desired hospitalization type</returns>
    Task<HospitalizationTypeDto> GetAsync(int id);
    /// <summary>
    /// Get all hospitalization types
    /// </summary>
    /// <returns>Hospitalization type list</returns>
    Task<PagedResultDto<HospitalizationTypeDto>> GetListAsync();
    /// <summary>
    /// Creates hospitalization type
    /// </summary>
    /// <param name="hospitalizationType">Hospitalization type information to be insert</param>
    /// <returns>Inserted hospitalization type</returns>
    Task<HospitalizationTypeDto> CreateAsync(SaveHospitalizationTypeDto hospitalizationType);

    /// <summary>
    /// Updates hospitalization type
    /// </summary>
    /// <param name="id">To be updated hospitalization type id</param>
    /// <param name="hospitalizationType">To be updated information</param>
    /// <returns>Updated nationality object</returns>
    Task<HospitalizationTypeDto> UpdateAsync(int id, SaveHospitalizationTypeDto hospitalizationType);

    /// <summary>
    /// Delete given id of hospitalization type
    /// </summary>
    /// <param name="id">To be deleted hospitalization type id</param>
    /// <returns></returns>
    Task DeleteAsync(int id);
}