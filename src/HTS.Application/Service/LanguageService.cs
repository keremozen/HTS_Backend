using HTS.Data.Entity;
using HTS.Dto.Language;
using HTS.Interface;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HTS.Service
{
    public class LanguageService : 
        CrudAppService<Language, LanguageDto,int,
        PagedAndSortedResultRequestDto,
        SaveLanguageDto>,
        ILanguageService
    {

        private readonly IRepository<Language, int> _languageRepository;
        public LanguageService(IRepository<Language, int> languageRepository) : base(languageRepository)
        {
            _languageRepository = languageRepository;

        }

        public override async Task<LanguageDto> CreateAsync(SaveLanguageDto input)
        {
           Language entity= ObjectMapper.Map<SaveLanguageDto, Language>(input);
           await _languageRepository.InsertAsync(entity);
            return ObjectMapper.Map<Language, LanguageDto>(entity);
        }

    }
}
