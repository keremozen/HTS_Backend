using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Interface;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using static HTS.Enum.EntityEnum;

namespace HTS.Service;

public class PatientService : ApplicationService, IPatientService
{
    private readonly IRepository<Patient, int> _patientRepository;
    public PatientService(IRepository<Patient, int> patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientDto> GetAsync(int id)
    {
        var query = (await _patientRepository.WithDetailsAsync()).Where(p => p.Id == id);
        var patient = await AsyncExecuter.FirstOrDefaultAsync(query);
        return ObjectMapper.Map<Patient, PatientDto>(patient);
    }

    public async Task<PagedResultDto<PatientDto>> GetListAsync()
    {
        //Get all entities
        var patientList = (await _patientRepository.WithDetailsAsync()).ToList();
        var patientDtoList = ObjectMapper.Map<List<Patient>, List<PatientDto>>(patientList);
        patientDtoList = patientDtoList.Select(p =>
        {
            p.PatientTreatmentProcesses = p.PatientTreatmentProcesses.OrderByDescending(t => t.Id).Take(1).ToList();
            return p;
        }).ToList();
        return new PagedResultDto<PatientDto>(patientDtoList.Count(),patientDtoList);
    }

    public async Task<PagedResultDto<PatientDto>> FilterListAsync(FilterPatientDto filter)
    {
        var query = await _patientRepository.WithDetailsAsync();
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(p => EF.Functions.ILike(p.Name, filter.Name));
        }
        if (!string.IsNullOrEmpty(filter.Surname))
        {
            query = query.Where(p => EF.Functions.ILike(p.Surname, filter.Surname));
        }
        if (!string.IsNullOrEmpty(filter.PassportNumber))
        {
            query = query.Where(p => EF.Functions.ILike(p.PassportNumber, filter.PassportNumber));
        }
        if (filter.PhoneCountryCodeIds?.Any() ?? false)
        {
            query = query.Where(p => p.PhoneCountryCodeId.HasValue && filter.PhoneCountryCodeIds.Contains(p.PhoneCountryCodeId.Value));
        }
        if (filter.NationalityIds?.Any() ?? false)
        {
            query = query.Where(p => filter.NationalityIds.Contains(p.NationalityId));
        }
        if (filter.GenderIds?.Any() ?? false)
        {
            query = query.Where(p => p.GenderId.HasValue && filter.GenderIds.Contains(p.GenderId.Value));
        }
        if (filter.MotherTongueIds?.Any() ?? false)
        {
            query = query.Where(p => p.MotherTongueId.HasValue && filter.MotherTongueIds.Contains(p.MotherTongueId.Value));
        }
        if (filter.SecondTongueIds?.Any() ?? false)
        {
            query = query.Where(p => p.SecondTongueId.HasValue && filter.SecondTongueIds.Contains(p.SecondTongueId.Value));
        }
        if (filter.PatientTreatmentProcessIds?.Any() ?? false)
        {
            query = query.Where(p => p.PatientTreatmentProcesses.Any(ptp => filter.PatientTreatmentProcessIds.Contains(ptp.Id)));
        }

        var patientList = await AsyncExecuter.ToListAsync(query);
        patientList = patientList.Select(p =>
            {
                if (filter.PatientTreatmentProcessIds?.Any() ?? false)
                {
                    p.PatientTreatmentProcesses =
                        p.PatientTreatmentProcesses.Where(t => filter.PatientTreatmentProcessIds.Contains(t.Id)).ToList();
                }
                else
                {
                    p.PatientTreatmentProcesses = p.PatientTreatmentProcesses.OrderByDescending(t => t.Id).Take(1).ToList();
                }
                return p;
            })
            .ToList();
        var responseList = ObjectMapper.Map<List<Patient>, List<PatientDto>>(patientList);
        var totalCount = await _patientRepository.CountAsync();//item count

        return new PagedResultDto<PatientDto>(totalCount, responseList);
    }
    public async Task<PatientDto> CreateAsync(SavePatientDto patient)
    {
        await IsDataValidToSave(patient);
        var entity = ObjectMapper.Map<SavePatientDto, Patient>(patient);
        var createdEntity = await _patientRepository.InsertAsync(entity, true);
        return ObjectMapper.Map<Patient, PatientDto>(createdEntity);
    }

    public async Task<PatientDto> UpdateAsync(int id, SavePatientDto patient)
    {
        await IsDataValidToSave(patient, id);
        var entity = await _patientRepository.GetAsync(id);
        ObjectMapper.Map(patient, entity);
        return ObjectMapper.Map<Patient, PatientDto>(await _patientRepository.UpdateAsync(entity));
    }

    public async Task DeleteAsync(int id)
    {
        await _patientRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="patient">To be saved object</param>
    /// <param name="id">Id in updated entity</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private async Task IsDataValidToSave(SavePatientDto patient, int? id = null)
    {
        //Check nationality and passportnumber is unique
        var query = await _patientRepository.GetQueryableAsync();
        query = query.Where(p => p.NationalityId == patient.NationalityId
                                 && !string.IsNullOrEmpty(p.PassportNumber)
                                 && p.PassportNumber == patient.PassportNumber)
            .WhereIf(id.HasValue,
                p => p.Id != id);
        if (await query.AnyAsync())
        {
            throw new HTSBusinessException(ErrorCode.NationalityAndPassportNumberMustBeUnique);
        }
    }


}