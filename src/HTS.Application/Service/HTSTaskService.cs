using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.HTSTask;
using HTS.Enum;
using HTS.Interface;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace HTS.Service;
public class HTSTaskService : ApplicationService, IHTSTaskService
{
    private readonly IRepository<HTSTask, int> _taskRepository;
    private readonly IRepository<HospitalPricer, int> _hospitalPricerRepository;
    private readonly ICurrentUser _currentUser;
    public HTSTaskService(IRepository<HTSTask, int> taskRepository,
        IRepository<HospitalPricer, int> hospitalPricerRepository,
        ICurrentUser currentUser) 
    {
        _taskRepository = taskRepository;
        _currentUser = currentUser;
        _hospitalPricerRepository = hospitalPricerRepository;
    }
    
    public async Task<PagedResultDto<HTSTaskDto>> GetListAsync()
    {
        //Get all entities
        var query = (await _taskRepository.WithDetailsAsync())
            .Where(t => t.UserId == _currentUser.Id);
        var responseList = ObjectMapper.Map<List<HTSTask>, List<HTSTaskDto>>(await AsyncExecuter.ToListAsync(query));
        var totalCount = await _taskRepository.CountAsync();//item count
        return new PagedResultDto<HTSTaskDto>(totalCount,responseList);
    }
    
    public async Task CreateAsync(SaveHTSTaskDto saveTask)
    {
        if (saveTask.TaskTypeId == EntityEnum.TaskTypeEnum.Pricing.GetHashCode())
        {
            if (saveTask.HospitalId.HasValue)
            {
                List<HTSTask> tasks = new List<HTSTask>();
                var pricers = (await _hospitalPricerRepository.GetQueryableAsync())
                    .Include(p => p.User)
                    .Where(p => p.HospitalId == saveTask.HospitalId).ToList();
                foreach (var pricer in pricers)
                {
                    tasks.Add(new HTSTask()
                    {
                        TaskTypeId = EntityEnum.TaskTypeEnum.Pricing.GetHashCode(),
                        UserId = pricer.UserId,
                        IsActive = true,
                        PatientId = saveTask.PatientId,
                        Url = "https://webhts.ushas.com.tr/patient/edit/"+saveTask.PatientId
                    });
                }
                await _taskRepository.InsertManyAsync(tasks);
               // List<string> toList = pricers.SelectMany(p => p.User.Email).ToList();
            }
           
        }

       

    }
}