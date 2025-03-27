using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class UserService:IUserService
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
            var user = await _userRepository.GetAllAsync();
            var filteredUsers = user.Where(t => t.Enabled); //Filtrar solo los que tienen Enabled = true
            return _mapper.Map<IEnumerable<UserDTO>>(filteredUsers);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {

            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task AddUserAsync(UserDTO userDto)
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

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                _userRepository.Delete(user);
                await _userRepository.SaveChangesAsync();
            }
        }

        public async Task<User> SearchUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var userDTObyEmail = await _userRepository.GetByEmail(user);
            return userDTObyEmail;
        }

        public async Task<string> GenerateJwtToken(string username)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin") 
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
    }
}
