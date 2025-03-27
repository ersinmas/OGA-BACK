using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO?> GetRoleByIdAsync(int id);
        Task AddRoleAsync(RoleDTO roleDto);
        Task UpdateRoleAsync(RoleDTO roleDTO);
        Task DeleteRoleAsync(int id);
    }
}
