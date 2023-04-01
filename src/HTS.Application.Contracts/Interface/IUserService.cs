using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace HTS.Interface
{
    public interface IUserService : IApplicationService
    {
        /// <summary>
        /// Get users by role
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>Users by given role name</returns>
        Task<IList<IdentityUserDto>> GetByRoleAsync(string roleName);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User by given id</returns>
        Task<IdentityUserDto> GetById(string id);
    }
}
