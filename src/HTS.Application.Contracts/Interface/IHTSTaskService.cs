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

        /// <summary>
        /// Assign user process to tik
        /// Task opens to tik users
        /// Email sends to tik users
        /// Patient entity sets as assigned to tik
        /// </summary>
        /// <param name="userId">The user to be assigned to tik</param>
        /// <returns></returns>
        Task AssignToTik(int userId);
        
        /// <summary>
        /// Get user process from tik
        /// Close tik users tasks
        /// Patient entity sets as returned from tik
        /// </summary>
        /// <param name="userId">The user to be returned from tik</param>
        /// <returns></returns>
        Task ReturnFromTik(int userId);

        /// <summary>
        /// Create task
        /// </summary>
        /// <param name="saveTask">To be created task object</param>
        /// <returns></returns>
        Task CreateAsync(SaveHTSTaskDto saveTask);

    }
}
