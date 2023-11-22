using System;
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
        /// Get proforma pricing list by patient treatment process id
        /// </summary>
        /// <param name="ptpId">patient treatment process id</param>
        /// <returns></returns>
        Task<List<ProformaPricingListDto>> GetPricingListByPTPIdAsync(int ptpId);

        /// <summary>
        /// Get proforma by id
        /// </summary>
        /// <param name="proformaId">Proforma ID</param>
        /// <returns>Proforma</returns>
        Task<ProformaDto> GetByIdAsync(int proformaId);

        /// <summary>
        /// Proforma insert with relational data
        /// </summary>
        /// <param name="proforma">To be inserted object</param>
        /// <returns>Proforma id</returns>
        Task<int> SaveAsync(SaveProformaDto proforma);

        /// <summary>
        /// Save proforma for enabiz
        /// </summary>
        /// <param name="proforma">To be inserted object</param>
        /// <returns>Proforma id</returns>
        Task<int> SaveENabizAsync(SaveENabizProformaDto proforma);

        /// <summary>
        /// Proforma send to mfb
        /// </summary>
        /// <param name="id">Proforma Id to be send</param>
        /// <returns></returns>
        Task SendAsync(int id);

        /// <summary>
        /// Approve proforma
        /// </summary>
        /// <param name="id">Proforma id to be approved</param>
        /// <returns></returns>
        Task ApproveMFBAsync(int id);
        
        /// <summary>
        /// Save proforma and Approve proforma
        /// </summary>
        /// <param name="proforma">Proforma to be saved and approved</param>
        /// <returns></returns>
        Task<Object> SaveAndApproveMFBAsync(SaveProformaDto proforma);

        /// <summary>
        /// Reject proforma
        /// </summary>
        /// <param name="rejectProforma">Reject object</param>
        /// <returns></returns>
        Task RejectMFBAsync(RejectProformaDto rejectProforma);

        /// <summary>
        /// Send proforma to patient
        /// </summary>
        /// <param name="id">Proforma id</param>
        /// <returns></returns>
        Task SendToPatient(int id);

        /// <summary>
        /// Patient approve proforma
        /// </summary>
        /// <param name="id">Proforma id</param>
        /// <returns></returns>
        Task ApprovePatientAsync(int id);
  

        /// <summary>
        /// Patient reject proforma
        /// </summary>
        /// <param name="rejectProforma">Reject object</param>
        /// <returns></returns>
        Task RejectPatientAsync(RejectProformaDto rejectProforma);

        /// <summary>
        /// Create proforma PDF
        /// </summary>
        /// <param name="id">Proforma id</param>
        /// <returns></returns>
        Task<ProformaPdfDto> CreateProformaPdf(int id);

    }
}
