using HTS.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Users;

public class ProcessedUserHelper : ApplicationService
{

    public ICachedServiceProvider _serviceProvider { get; set; }
    IDbContextProvider<AppDbContext> _sampleDbContextProvider { get; set; }
    private readonly IIdentityUserRepository _userRepository;
    public ProcessedUserHelper(ICachedServiceProvider serviceProvider, 
        IDbContextProvider<AppDbContext> sampleDbContextProvider,
        IIdentityUserRepository userRepository)
    {
        _serviceProvider = serviceProvider;
        _sampleDbContextProvider = sampleDbContextProvider;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Load creator information to list of auditedentitywithuserdto inherited class of objects
    /// </summary>
    /// <param name="_userRepository">User repository instance to query identity users</param>
    /// <param name="dtoList">List of dto items to add creator information</param>
    public async void LoadCreator<T>(IIdentityUserRepository _userRepository,
            List<T> dtoList) where T : AuditedEntityWithUserDto<int, IdentityUserDto>
    {
        //userRepository = LazyServiceProvider.GetRequiredService<IRepository<IdentityUser, Guid>>();
        var userRepositor2 = _serviceProvider.GetRequiredService<IRepository<IdentityUser, Guid>>();
        //List of identity users
        Dictionary<Guid, IdentityUserDto> identityUsers = new Dictionary<Guid, IdentityUserDto>();
        foreach (var dto in dtoList)//Check every item if contains creator information
        {
            if (dto.CreatorId.HasValue)
            {//Set creator information
                if (identityUsers.ContainsKey(dto.CreatorId.Value))//Already exist
                {
                    dto.Creator = identityUsers[dto.CreatorId.Value];
                }
                else
                {//Get creator from db
                 //   var user = await userRepository.FindAsync(dto.CreatorId.Value);
                   var user = await _userRepository.FindAsync(dto.CreatorId.Value);
                     user = await userRepositor2.FindAsync(dto.CreatorId.Value);
                    var creatorUser = ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
                    dto.Creator = creatorUser;
                    identityUsers.Add(creatorUser.Id, creatorUser);
                }
            }
        }
    }
}