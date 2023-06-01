using System.Threading.Tasks;
using HTS.Dto.HospitalResponse;
using HTS.Dto.Operation;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IOperationService : IApplicationService
    {
       
        /// <summary>
        /// Manual operation insert with relational data
        /// </summary>
        /// <param name="operation">Operation data to be insert</param>
        /// <returns></returns>
        Task CreateAsync(SaveOperationDto operation);

     
    }
}
