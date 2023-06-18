using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalResponse;
using HTS.Dto.Operation;
using HTS.Dto.Proforma;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HTS.Interface
{
    public interface IProformaService : IApplicationService
    {
       
        /// <summary>
        /// Get proformanamelist by operation
        /// </summary>
        /// <param name="operationId">Operation</param>
        /// <returns>Response list</returns>
        Task<List<ProformaListDto>> GetNameListByOperationIdAsync(int operationId);


        /// <summary>
        /// Proforma insert with relational data
        /// </summary>
        /// <param name="proforma">To be inserted object</param>
        /// <returns></returns>
        Task SaveAsync(SaveProformaDto proforma);
       

    }
}
