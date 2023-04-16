using System.Collections.Generic;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Interface;
using System.Threading.Tasks;
using HTS.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HTS.Dto.City;

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
            return new PagedResultDto<LanguageDto>(totalCount, responseList);
        }

        public async Task<LanguageDto> CreateAsync(SaveLanguageDto language)
        {
            var entity = ObjectMapper.Map<SaveLanguageDto, Language>(language);
            await _languageRepository.InsertAsync(entity);
            return ObjectMapper.Map<Language, LanguageDto>(entity);
        }

        public async Task<LanguageDto> UpdateAsync(int id, SaveLanguageDto language)
        {
            var entity = await _languageRepository.GetAsync(id);
            ObjectMapper.Map(language, entity);
            return ObjectMapper.Map<Language, LanguageDto>(await _languageRepository.UpdateAsync(entity));
        }

        public async Task DeleteAsync(int id)
        {
            await _languageRepository.DeleteAsync(id);
        }
    }
}
