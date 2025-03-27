using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserRoleService:IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserRoleDTO>> GetAllUserRolesAsync()
        {
            var userRole = await _userRoleRepository.GetAllAsync();
            var filteredUserRoles = userRole.Where(t => t.Enabled); //Filtrar solo los que tienen Enabled = true
            return _mapper.Map<IEnumerable<UserRoleDTO>>(filteredUserRoles);
        }

        public async Task<UserRoleDTO?> GetUserRoleByIdAsync(int id)
        {
            throw new NotImplementedException();

        }

        public async Task AddUserRoleAsync(UserRoleDTO userRoleDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleDto);
            await _userRoleRepository.AddAsync(userRole);
            await _userRoleRepository.SaveChangesAsync();
        }

        public async Task UpdateUserRoleAsync(UserRoleDTO userRoleDto)
        {
            throw new NotImplementedException();

        }

        public async Task DeleteUserRoleAsync(UserRoleDTO userRoleDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleDto);
            if (userRole != null)
            {
                _userRoleRepository.Delete(userRole);
                await _userRoleRepository.SaveChangesAsync();
            }
        }

        public Task DeleteUserRoleAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
