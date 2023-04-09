using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    private readonly IIdentityUserRepository _userRepository;
    public PatientService(IRepository<Patient, int> patientRepository, IIdentityUserRepository userRepository)
    {
        _patientRepository = patientRepository;
        _userRepository = userRepository;
    }

    public async Task<PatientDto> GetAsync(int id)
    {
        var query = (await _patientRepository.WithDetailsAsync()).Where(p => p.Id == id);
        var patient = await AsyncExecuter.FirstOrDefaultAsync(query);
        patient.PatientTreatmentProcesses = patient.PatientTreatmentProcesses.OrderByDescending(t => t.Id).Take(1).ToList();
        return ObjectMapper.Map<Patient, PatientDto>(patient);
    }

    public async Task<PagedResultDto<PatientDto>> GetListAsync()
    {
        //Get all entities
        var query = await _patientRepository.WithDetailsAsync();
        var patientList = await AsyncExecuter.ToListAsync(query);
        patientList = patientList.Select(p =>
        {
            p.PatientTreatmentProcesses =  p.PatientTreatmentProcesses.OrderByDescending(t => t.Id).Take(1).ToList();
            return p;
        })
        .ToList();
        var responseList = ObjectMapper.Map<List<Patient>, List<PatientDto>>(patientList);
        var totalCount = await _patientRepository.CountAsync();//item count

        return new PagedResultDto<PatientDto>(totalCount, responseList);
    }

    public async Task<PatientDto> CreateAsync(SavePatientDto patient)
    {
        var entity = ObjectMapper.Map<SavePatientDto, Patient>(patient);
        var createdEntity = await _patientRepository.InsertAsync(entity, true);
        return ObjectMapper.Map<Patient, PatientDto>(createdEntity);
    }

    public async Task<PatientDto> UpdateAsync(int id, SavePatientDto patient)
    {
        var entity = await _patientRepository.GetAsync(id);
        ObjectMapper.Map(patient, entity);
        return ObjectMapper.Map<Patient, PatientDto>(await _patientRepository.UpdateAsync(entity));
    }

    public async Task DeleteAsync(int id)
    {
        await _patientRepository.DeleteAsync(id);
    }
    //TODO:Hopsy check nationality and passportnumber unique

}