using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDTO>> GetAllUserRolesAsync();
        Task<UserRoleDTO?> GetUserRoleByIdAsync(int id);
        Task AddUserRoleAsync(UserRoleDTO userRoleDto);
        Task UpdateUserRoleAsync(UserRoleDTO userRoleDto);
        Task DeleteUserRoleAsync(UserRoleDTO userRoleDto);
    }
}
