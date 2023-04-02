using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

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
        return ObjectMapper.Map<Patient, PatientDto>(await _patientRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<PatientDto>> GetListAsync()
    {
        //Get all entities
        var responseList = ObjectMapper.Map<List<Patient>, List<PatientDto>>(await _patientRepository.GetListAsync());
        var totalCount = await _patientRepository.CountAsync();//item count
       
        Dictionary<Guid, IdentityUserDto> identityUsers = new Dictionary<Guid, IdentityUserDto>();
        foreach (var dto in responseList)//Check every item if contains creator information
        {
            if (dto.CreatorId.HasValue)
            {//Set creator information
                if (identityUsers.ContainsKey(dto.CreatorId.Value))//Already exist
                {
                    dto.Creator = identityUsers[dto.CreatorId.Value];
                }
                else
                {//Get creator from db
                    var creatorUser = ObjectMapper.Map<IdentityUser, IdentityUserDto>(await _userRepository.FindAsync(dto.CreatorId.Value));
                    dto.Creator = creatorUser;
                    identityUsers.Add(creatorUser.Id, creatorUser);
                }
            }
        }
        return new PagedResultDto<PatientDto>(totalCount, responseList);
    }

    public async Task<PatientDto> CreateAsync(SavePatientDto patient)
    {
        var entity = ObjectMapper.Map<SavePatientDto, Patient>(patient);
        var createdEntity = await _patientRepository.InsertAsync(entity);
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