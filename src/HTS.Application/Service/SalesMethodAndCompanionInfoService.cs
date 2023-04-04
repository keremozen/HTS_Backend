using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientNote;
using HTS.Dto.SalesMethodAndCompanionInfo;
using HTS.Interface;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class SalesMethodAndCompanionInfoService : ApplicationService, ISalesMethodAndCompanionInfoService
{
    private readonly IRepository<SalesMethodAndCompanionInfo, int> _salesMethodAndCompanionInfoRepository;
    private readonly IIdentityUserRepository _userRepository;
    public SalesMethodAndCompanionInfoService(IRepository<SalesMethodAndCompanionInfo, int> salesMethodAndCompanionInfoRepository,
        IIdentityUserRepository userRepository)
    {
        _salesMethodAndCompanionInfoRepository = salesMethodAndCompanionInfoRepository;
        _userRepository = userRepository;
    }

    public async Task<SalesMethodAndCompanionInfoDto> GetByPatientTreatmentProcessIdAsync(int ptpId)
    {
        var response = await _salesMethodAndCompanionInfoRepository.GetAsync(i => i.PatientTreatmentProcessId == ptpId);
        return ObjectMapper.Map<SalesMethodAndCompanionInfo, SalesMethodAndCompanionInfoDto>(response);
    }

    public async Task<SalesMethodAndCompanionInfoDto> SaveAsync(SaveSalesMethodAndCompanionInfoDto salesMethodAndCompanionInfo)
    {
        bool isAnyInDB = await _salesMethodAndCompanionInfoRepository.AnyAsync(i =>
                  i.PatientTreatmentProcessId == salesMethodAndCompanionInfo.PatientTreatmentProcessId);
        if (isAnyInDB)
        {
            throw new UserFriendlyException(
                "There is already a sales method and companion information in the system."
            );
        }
        var entity = ObjectMapper.Map<SaveSalesMethodAndCompanionInfoDto, SalesMethodAndCompanionInfo>(salesMethodAndCompanionInfo);
        await _salesMethodAndCompanionInfoRepository.InsertAsync(entity);
        return ObjectMapper.Map<SalesMethodAndCompanionInfo, SalesMethodAndCompanionInfoDto>(entity);
    }


}