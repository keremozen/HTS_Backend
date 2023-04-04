using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using HTS.Dto.PatientNote;
using HTS.Dto.PatientTreatmentProcess;
using HTS.Dto.SalesMethodAndCompanionInfo;

namespace HTS.Interface
{
    public interface ISalesMethodAndCompanionInfoService : IApplicationService
    {
      
        /// <summary>
        /// Get sales method and companion info by patient treatment process id
        /// </summary>
        /// <param name="ptpId">Patient treatment process id</param>
        /// <returns>Sales method and companion info</returns>
        Task<SalesMethodAndCompanionInfoDto> GetByPatientTreatmentProcessIdAsync(int ptpId);
        
        /// <summary>
        /// Creates sales method and companion info
        /// </summary>
        /// <param name="salesMethodAndCompanionInfo">Sales method and companion info to be insert</param>
        /// <returns>Inserted sales method and companion info</returns>
        Task<SalesMethodAndCompanionInfoDto> SaveAsync(SaveSalesMethodAndCompanionInfoDto salesMethodAndCompanionInfo);
        
    }
}
