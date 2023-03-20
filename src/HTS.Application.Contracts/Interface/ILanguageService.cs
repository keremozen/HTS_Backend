using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface ILanguageService : ICrudAppService<LanguageDto, 
        int,
        PagedAndSortedResultRequestDto,
        SaveLanguageDto>
    {
       
    }
}
