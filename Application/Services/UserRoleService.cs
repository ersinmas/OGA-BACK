using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserRoleService : IUserRoleService
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
            try
            {
                var userRole = await _userRoleRepository.GetAllAsync();
                var filteredUserRoles = userRole.Where(t => t.Enabled); 
                return _mapper.Map<IEnumerable<UserRoleDTO>>(filteredUserRoles);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los roles de usuario", ex);
            }
        }

        public async Task<UserRoleDTO?> GetUserRoleByIdAsync(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el rol de usuario por ID", ex);
            }
        }

        public async Task AddUserRoleAsync(UserRoleDTO userRoleDto)
        {
            try
            {
                var userRole = _mapper.Map<UserRole>(userRoleDto);
                await _userRoleRepository.AddAsync(userRole);
                await _userRoleRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al agregar el rol de usuario", ex);
            }
        }

        public async Task UpdateUserRoleAsync(UserRoleDTO userRoleDto)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el rol de usuario", ex);
            }
        }

        public async Task DeleteUserRoleAsync(UserRoleDTO userRoleDto)
        {
            try
            {
                var userRole = _mapper.Map<UserRole>(userRoleDto);
                if (userRole != null)
                {
                    _userRoleRepository.Delete(userRole);
                    await _userRoleRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el rol de usuario", ex);
            }
        }

        public Task DeleteUserRoleAsync(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el rol de usuario por ID", ex);
            }
        }
    }
}
