using System.Collections.Generic;
using System.Linq;
using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<PagedResultDto<LanguageDto>> GetListAsync(bool? isActive=null)
        {
            var query = await _languageRepository.GetQueryableAsync();
            query = query.WhereIf(isActive.HasValue,
                b => b.IsActive == isActive.Value);
            var responseList = ObjectMapper.Map<List<Language>, List<LanguageDto>>(await AsyncExecuter.ToListAsync(query));
            var totalCount = await _languageRepository.CountAsync();//item count
            return new PagedResultDto<LanguageDto>(totalCount, responseList);
        }

        [Authorize("HTS.LanguageManagement")]
        public async Task<LanguageDto> CreateAsync(SaveLanguageDto language)
        {
            var entity = ObjectMapper.Map<SaveLanguageDto, Language>(language);
            await _languageRepository.InsertAsync(entity);
            return ObjectMapper.Map<Language, LanguageDto>(entity);
        }

        [Authorize("HTS.LanguageManagement")]
        public async Task CreateListAsync(List<SaveLanguageDto> languages)
        {
            var entityList = ObjectMapper.Map<List<SaveLanguageDto>, List<Language>>(languages);
            await _languageRepository.InsertManyAsync(entityList);
        }

        [Authorize("HTS.LanguageManagement")]
        public async Task<LanguageDto> UpdateAsync(int id, SaveLanguageDto language)
        {
            var entity = await _languageRepository.GetAsync(id);
            ObjectMapper.Map(language, entity);
            return ObjectMapper.Map<Language, LanguageDto>(await _languageRepository.UpdateAsync(entity));
        }

        [Authorize("HTS.LanguageManagement")]
        public async Task DeleteAsync(int id)
        {
            await _languageRepository.DeleteAsync(id);
        }
    }
}
