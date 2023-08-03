using System.Threading.Tasks;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalResponse;
using HTS.Dto.Operation;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IOperationService : IApplicationService
    {
        /// <summary>
        /// Get operation by id
        /// </summary>
        /// <param name="id">Desired operation id</param>
        /// <returns>Desired operation</returns>
        Task<OperationDto> GetAsync(int id);

        /// <summary>
        /// Get entity list by patient treatment process
        /// </summary>
        /// <param name="ptpId">Patient treatment process id</param>
        /// <returns>Operation list by ptp</returns>
        public Task<PagedResultDto<OperationDto>> GetByPatientTreatmenProcessAsync(int ptpId);
        /// <summary>
        /// Manual operation insert with relational data
        /// </summary>
        /// <param name="operation">Operation data to be insert</param>
        /// <returns></returns>
        Task CreateAsync(SaveOperationDto operation);
        /// <summary>
        /// Hospital consultation type operation data update
        /// </summary>
        /// <param name="operation">Operation data to be updated</param>
        /// <returns></returns>
        Task UpdateAsync(int id, SaveOperationDto operation);

        /// <summary>
        /// Send to pricing
        /// </summary>
        /// <param name="id">To be send entity id</param>
        /// <returns></returns>
        Task SendToPricing(int id);


    }
}
