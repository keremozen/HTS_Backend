using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.Operation;
using HTS.Dto.Proforma;
using HTS.Enum;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using static HTS.Enum.EntityEnum;
namespace HTS.Service;
[Authorize]
public class ProformaService : ApplicationService, IProformaService
{
    private readonly IRepository<Proforma, int> _proformaRepository;
    private readonly IRepository<PatientTreatmentProcess, int> _patientTreatmentProcessRepository;

    public ProformaService(IRepository<Proforma, int> proformaRepository,
        IRepository<PatientTreatmentProcess, int> patientTreatmentProcessRepository)
    {
        _proformaRepository = proformaRepository;
        _patientTreatmentProcessRepository = patientTreatmentProcessRepository;
    }

    public async Task<List<ProformaListDto>> GetNameListByOperationIdAsync(int operationId)
    {
        var query = await (await _proformaRepository.WithDetailsAsync(p => p.Creator))
            .Where(p => p.OperationId == operationId).ToListAsync();
       return ObjectMapper.Map<List<Proforma>, List<ProformaListDto>>(query);
    }
    


    public async Task SaveAsync(SaveProformaDto proforma)
    {
        IsDataValidToSave(proforma);
        var entity = ObjectMapper.Map<SaveProformaDto, Proforma>(proforma);
        entity.Version = await GetVersion(entity);;
        entity.CreationDate = DateTime.Now;
        //TODO:Hopsy do calculations
        await _proformaRepository.InsertAsync(entity);
    }

    private async Task<int> GetVersion(Proforma entity)
    {
        var query = await _proformaRepository.GetQueryableAsync();
        int version = query.Where(p => p.OperationId == entity.OperationId).Max(p => p == null ? 0 : p.Version);
        return ++version;
    }
    

    /// <summary>
    /// Checks if data is valid to save
    /// </summary>
    /// <param name="proforma">To be saved object</param>
    /// <exception cref="HTSBusinessException">Check response exceptions</exception>
    private void IsDataValidToSave(SaveProformaDto proforma)
    {
        //TODO: validasyonları ekle
    }


}