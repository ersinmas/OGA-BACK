using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class RoleService : IRoleService
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
            try
            {
                var role = await _roleRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<RoleDTO>>(role);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los roles", ex);
            }
        }

        public async Task<RoleDTO?> GetRoleByIdAsync(int id)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(id);
                return role != null ? _mapper.Map<RoleDTO>(role) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el rol por ID", ex);
            }
        }

        public async Task AddRoleAsync(RoleDTO roleDto)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDto);
                await _roleRepository.AddAsync(role);
                await _roleRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al agregar el rol", ex);
            }
        }

        public async Task UpdateRoleAsync(RoleDTO roleDto)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDto);
                _roleRepository.Update(role);
                await _roleRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el rol", ex);
            }
        }

        public async Task DeleteRoleAsync(int id)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(id);
                if (role != null)
                {
                    _roleRepository.Delete(role);
                    await _roleRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el rol", ex);
            }
        }
    }
}
