using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

public class ProcessedUserHelper : ApplicationService
{

    
    /// <summary>
    /// Load creator information to list of auditedentitywithuserdto inherited class of objects
    /// </summary>
    /// <param name="_userRepository">User repository instance to query identity users</param>
    /// <param name="dtoList">List of dto items to add creator information</param>
    public async  void LoadCreator<T>(IIdentityUserRepository _userRepository,
        List<T> dtoList ) where T : AuditedEntityWithUserDto<int, IdentityUserDto>
    {
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
                    var creatorUser = ObjectMapper.Map<IdentityUser, IdentityUserDto>(await _userRepository.FindAsync(dto.CreatorId.Value));
                    dto.Creator = creatorUser;
                    identityUsers.Add(creatorUser.Id, creatorUser);
                }
            }
        }
    }
}