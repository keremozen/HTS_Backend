using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTS.Data.Entity;
using HTS.Dto.HTSTask;
using HTS.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace HTS.Service;
public class HTSTaskService : ApplicationService, IHTSTaskService
{
    private readonly IRepository<HTSTask, int> _taskRepository;
    private readonly ICurrentUser _currentUser;
    public HTSTaskService(IRepository<HTSTask, int> taskRepository,
        ICurrentUser currentUser) 
    {
        _taskRepository = taskRepository;
        _currentUser = currentUser;
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
}