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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;
[Authorize]
public class SalesMethodAndCompanionInfoService : ApplicationService, ISalesMethodAndCompanionInfoService
{
    private readonly IRepository<SalesMethodAndCompanionInfo, int> _salesMethodAndCompanionInfoRepository;
    private readonly IIdentityUserRepository _userRepository;
    private readonly IRepository<InterpreterAppointment, int> _interpreterAppointmentRepository;
    public SalesMethodAndCompanionInfoService(IRepository<SalesMethodAndCompanionInfo, int> salesMethodAndCompanionInfoRepository,
        IIdentityUserRepository userRepository,
        IRepository<InterpreterAppointment, int> interpreterAppointmentRepository)
    {
        _salesMethodAndCompanionInfoRepository = salesMethodAndCompanionInfoRepository;
        _userRepository = userRepository;
        _interpreterAppointmentRepository = interpreterAppointmentRepository;
    }

    public async Task<SalesMethodAndCompanionInfoDto> GetByPatientTreatmentProcessIdAsync(int ptpId)
    {
        try
        {
            var response =await (await _salesMethodAndCompanionInfoRepository.GetQueryableAsync())
                .AsNoTracking()
                .Include(s => s.InterpreterAppointments)
                .FirstOrDefaultAsync(i => i.PatientTreatmentProcessId == ptpId);
            return ObjectMapper.Map<SalesMethodAndCompanionInfo, SalesMethodAndCompanionInfoDto>(response);
        }
        catch (EntityNotFoundException)
        {
            return null;
        }
    }

    public async Task<SalesMethodAndCompanionInfoDto> SaveAsync(SaveSalesMethodAndCompanionInfoDto salesMethodAndCompanionInfo)
    {
        var entity = await _salesMethodAndCompanionInfoRepository.FirstOrDefaultAsync(i =>
                  i.PatientTreatmentProcessId == salesMethodAndCompanionInfo.PatientTreatmentProcessId);
        if (entity != null)
        {//Update process
            //Delete child records
            await _interpreterAppointmentRepository.DeleteManyAsync(entity.InterpreterAppointments.Select(a => a.Id).ToList());
            ObjectMapper.Map(salesMethodAndCompanionInfo, entity);
            await _salesMethodAndCompanionInfoRepository.UpdateAsync(entity);
        }
        else
        {
            entity = ObjectMapper.Map<SaveSalesMethodAndCompanionInfoDto, SalesMethodAndCompanionInfo>(salesMethodAndCompanionInfo);
            await _salesMethodAndCompanionInfoRepository.InsertAsync(entity);
        }
       
        return ObjectMapper.Map<SalesMethodAndCompanionInfo, SalesMethodAndCompanionInfoDto>(entity);
    }


}