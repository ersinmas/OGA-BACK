using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var user = await _userRepository.GetAllAsync();
                var filteredUsers = user.Where(t => t.Enabled); 
                return _mapper.Map<IEnumerable<UserDTO>>(filteredUsers);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los usuarios", ex);
            }
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                return user != null ? _mapper.Map<UserDTO>(user) : null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el usuario por ID", ex);
            }
        }

        public async Task AddUserAsync(UserDTO userDto)
        {
            try
            {
                var users = await this.GetAllUsersAsync();
                foreach (var u in users)
                {
                    if (u.Email == userDto.Email)
                    {
                        throw new InvalidOperationException("El correo electrónico ya está registrado.");
                    }
                }

                var user = _mapper.Map<User>(userDto);
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al agregar el usuario", ex);
            }
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el usuario", ex);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user != null)
                {
                    _userRepository.Delete(user);
                    await _userRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el usuario", ex);
            }
        }

        public async Task<User> SearchUser(UserDTO userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var userDTObyEmail = await _userRepository.GetByEmail(user);
                return userDTObyEmail;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar el usuario por correo", ex);
            }
        }

        public async Task<string> GenerateJwtToken(string email)
        {
            try
            {
                var jwtSettings = _config.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, email), 
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),  
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al generar el token JWT", ex);
            }
        }
    }
}
