using System.Collections.Generic;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Interface;
using System.Threading.Tasks;
using HTS.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    public class LanguageService : ApplicationService, ILanguageService
    {

        private readonly IRepository<Language, int> _languageRepository;
        public LanguageService(IRepository<Language, int> languageRepository) 
        {
            _languageRepository = languageRepository;
        }
        
        public async Task<LanguageDto> GetAsync(int id)
        {
            return ObjectMapper.Map<Language, LanguageDto>(await _languageRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<LanguageDto>> GetListAsync()
        {
            //Get all entities
            var responseList = ObjectMapper.Map<List<Language>, List<LanguageDto>>(await _languageRepository.GetListAsync());
            var totalCount = await _languageRepository.CountAsync();//item count
            //TODO:Hopsy Ask Kerem the isActive case 
            return new PagedResultDto<LanguageDto>(totalCount,responseList);
        }

        public Task<LanguageDto> CreateAsync(SaveLanguageDto language)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(int id, SaveLanguageDto language)
        {
            throw new System.NotImplementedException();
        }
        
        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
