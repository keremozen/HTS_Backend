using AutoMapper;
using HTS.Data.Entity;
using HTS.Dto;
using HTS.Dto.Gender;
using HTS.Dto.Language;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace HTS;

public class HTSApplicationAutoMapperProfile : Profile
{
    public HTSApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Gender, GenderDto>();
        CreateMap<Language,LanguageDto>();
        CreateMap<SaveLanguageDto, Language>();
    }
}
