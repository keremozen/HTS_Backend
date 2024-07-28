using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.FinalizationType;
using HTS.Dto.Nationality;
using HTS.Dto.PaymentReason;
using HTS.Dto.ProcessKind;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    public class FinalizationTypeService : ApplicationService, IFinalizationTypeService
    {
        private readonly IRepository<FinalizationType, int> _ftRepository;

        public FinalizationTypeService(IRepository<FinalizationType, int> ftRepository) 
        {
            _ftRepository = ftRepository;
        }
        
        public async Task<FinalizationTypeDto> GetAsync(int id)
        {
            return ObjectMapper.Map<FinalizationType, FinalizationTypeDto>(await _ftRepository.GetAsync(id));
        }

        public async Task<ListResultDto<FinalizationTypeDto>> GetListAsync()
        {
            var responseList = ObjectMapper.Map<List<FinalizationType>, List<FinalizationTypeDto>>(await _ftRepository.GetListAsync());
            return new ListResultDto<FinalizationTypeDto>(responseList);
        }

        [Authorize("HTS.FinalizationTypeManagement")]
        public async Task CreateAsync(SaveFinalizationTypeDto finalizationType)
        {
            var entity = ObjectMapper.Map<SaveFinalizationTypeDto, FinalizationType>(finalizationType);
            await _ftRepository.InsertAsync(entity);
        }

        [Authorize("HTS.FinalizationTypeManagement")]
        public async Task UpdateAsync(int id, SaveFinalizationTypeDto finalizationType)
        {
            var entity = await _ftRepository.GetAsync(id);
            ObjectMapper.Map(finalizationType, entity);
            await _ftRepository.UpdateAsync(entity);
        }

        [Authorize("HTS.FinalizationTypeManagement")]
        public async Task DeleteAsync(int id)
        {
            await _ftRepository.DeleteAsync(id);
        }
    }
}