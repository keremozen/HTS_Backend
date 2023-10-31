using HTS.Dto;
using HTS.Dto.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HTS.Dto.Hospital;
using HTS.Dto.HTSTask;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace HTS.Interface
{
    public interface IHTSTaskService : IApplicationService
    {
        /// <summary>
        /// Get all tasks of current user
        /// </summary>
        /// <returns>Task list</returns>
        Task<PagedResultDto<HTSTaskDto>> GetListAsync();
      
    }
}
