using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.BusinessException;
using HTS.Data.Entity;
using HTS.Dto.HospitalInterpreter;
using HTS.Dto.HospitalPricer;
using HTS.Interface;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace HTS.Service;

 [Authorize("HTS.HospitalManagement")]
    public class HospitalInterpreterService : ApplicationService, IHospitalInterpreterService
    {
        private readonly IRepository<HospitalInterpreter, int> _hospitalInterpreterRepository;
        private IIdentityUserRepository _userRepository;

        public HospitalInterpreterService(IRepository<HospitalInterpreter, int> hospitalInterpreterRepository, IIdentityUserRepository userRepository)
        {
            _hospitalInterpreterRepository = hospitalInterpreterRepository;
            _userRepository = userRepository;
        }

        public async Task<PagedResultDto<HospitalInterpreterDto>> GetByHospitalListAsync(int hospitalId, bool? isActive = null)
        {
            var query = (await _hospitalInterpreterRepository.GetQueryableAsync())
                .Where(s => s.HospitalId == hospitalId)
                .WhereIf(isActive.HasValue, s => s.IsActive == isActive.Value);

            var totalCount = await _hospitalInterpreterRepository.CountAsync();
            var responseList = ObjectMapper.Map<List<HospitalInterpreter>, List<HospitalInterpreterDto>>(await AsyncExecuter.ToListAsync(query));
            return new PagedResultDto<HospitalInterpreterDto>(totalCount, responseList);
        }

        public async Task CreateAsync(SaveHospitalInterpreterDto hospitalInterpreter)
        {
            await IsDataValidToSave(hospitalInterpreter);
            var entity = ObjectMapper.Map<SaveHospitalInterpreterDto, HospitalInterpreter>(hospitalInterpreter);
            entity.IsDefault = entity.IsActive && entity.IsDefault;
            await _hospitalInterpreterRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(int id, SaveHospitalInterpreterDto hospitalInterpreter)
        {
            await IsDataValidToSave(hospitalInterpreter, id);
            var entity = await _hospitalInterpreterRepository.GetAsync(id);
            ObjectMapper.Map(hospitalInterpreter, entity);
            entity.IsDefault = entity.IsActive && entity.IsDefault;
            await _hospitalInterpreterRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _hospitalInterpreterRepository.DeleteAsync(id);
        }

        private async Task IsDataValidToSave(SaveHospitalInterpreterDto hospitalInterpreter, int? id = null)
        {
            if (!id.HasValue 
                && (await _hospitalInterpreterRepository.GetQueryableAsync()).Any(s => s.UserId == hospitalInterpreter.UserId 
                    && s.HospitalId == hospitalInterpreter.HospitalId))
            {
                throw new HTSBusinessException(ErrorCode.InterpreterAlreadyExist);
            }

            if (hospitalInterpreter.IsDefault)
            {
                if ((await _hospitalInterpreterRepository.GetQueryableAsync()).Any(s => s.IsActive
                        && s.IsDefault
                        && (!id.HasValue || s.Id != id) 
                        && s.HospitalId == hospitalInterpreter.HospitalId))
                {
                    throw new HTSBusinessException(ErrorCode.DefaultInterpreterAlreadyExist);
                }
            }
        }
    }