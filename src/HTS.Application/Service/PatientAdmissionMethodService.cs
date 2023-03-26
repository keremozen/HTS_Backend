using System.Collections.Generic;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Dto.PatientAdmissionMethod;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service;

public class PatientAdmissionMethodService : ApplicationService, IPatientAdmissionMethodService
{
    private readonly IRepository<PatientAdmissionMethod, int> _patientAdmissionMethodRepository;
    public PatientAdmissionMethodService(IRepository<PatientAdmissionMethod, int> patientAdmissionMethodRepository) 
    {
        _patientAdmissionMethodRepository = patientAdmissionMethodRepository;
    }
    
    public async Task<PatientAdmissionMethodDto> GetAsync(int id)
    {
        return ObjectMapper.Map<PatientAdmissionMethod, PatientAdmissionMethodDto>(await _patientAdmissionMethodRepository.GetAsync(id));
    }

    public async Task<PagedResultDto<PatientAdmissionMethodDto>> GetListAsync()
    {
        //Get all entities
        var responseList = ObjectMapper.Map<List<PatientAdmissionMethod>, List<PatientAdmissionMethodDto>>(await _patientAdmissionMethodRepository.GetListAsync());
        var totalCount = await _patientAdmissionMethodRepository.CountAsync();//item count
        //TODO:Hopsy Ask Kerem the isActive case 
        return new PagedResultDto<PatientAdmissionMethodDto>(totalCount,responseList);
    }

    public async Task<PatientAdmissionMethodDto> CreateAsync(SavePatientAdmissionMethodDto patientAdmissionMethod)
    {
        var entity = ObjectMapper.Map<SavePatientAdmissionMethodDto, PatientAdmissionMethod>(patientAdmissionMethod);
        await _patientAdmissionMethodRepository.InsertAsync(entity);
        return ObjectMapper.Map<PatientAdmissionMethod, PatientAdmissionMethodDto>(entity);
    }

    public async Task<PatientAdmissionMethodDto> UpdateAsync(int id, SavePatientAdmissionMethodDto patientAdmissionMethod)
    {
        var entity = await _patientAdmissionMethodRepository.GetAsync(id);
        ObjectMapper.Map(patientAdmissionMethod, entity);
        return ObjectMapper.Map<PatientAdmissionMethod, PatientAdmissionMethodDto>( await _patientAdmissionMethodRepository.UpdateAsync(entity));
    }
        
    public async Task DeleteAsync(int id)
    {
        await _patientAdmissionMethodRepository.DeleteAsync(id);
    }

}