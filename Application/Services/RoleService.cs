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
    public class RoleService:IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            var role = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDTO>>(role);
        }

        public async Task<RoleDTO?> GetRoleByIdAsync(int id)
        {

            var role = await _roleRepository.GetByIdAsync(id);
            return role != null ? _mapper.Map<RoleDTO>(role) : null;
        }

        public async Task AddRoleAsync(RoleDTO roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(RoleDTO roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            _roleRepository.Update(role);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role != null)
            {
                _roleRepository.Delete(role);
                await _roleRepository.SaveChangesAsync();
            }
        }
    }
}
