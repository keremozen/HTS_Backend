using HTS.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace HTS.Service
{
    [Authorize]
    public class UserService : ApplicationService, IUserService
    {
        private IdentityUserManager _userManager;
        public UserService(IdentityUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUserDto> GetById(string id)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(await _userManager.FindByIdAsync(id));
        }

        public async Task<IList<IdentityUserDto>> GetByRoleAsync(string roleName)
        {
            return ObjectMapper.Map<IList<IdentityUser>, IList<IdentityUserDto>>(await _userManager.GetUsersInRoleAsync(roleName));
        }
    }
}
