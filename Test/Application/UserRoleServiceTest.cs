using Moq;
using Application.Services;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Application.Tests
{
    public class UserRoleServiceTests
    {
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserRoleService _userRoleService;

        public UserRoleServiceTests()
        {
            _mockUserRoleRepository = new Mock<IUserRoleRepository>();
            _mockMapper = new Mock<IMapper>();
            _userRoleService = new UserRoleService(_mockUserRoleRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllUserRolesAsync_ShouldReturnListOfUserRoles()
        {
            // Arrange
            var userRoleEntities = new List<UserRole>
            {
                new UserRole { UserId = 1, RoleId = 1, Enabled = true },
                new UserRole { UserId = 2, RoleId = 2, Enabled = true }
            };
            var userRoleDtos = new List<UserRoleDTO>
            {
                new UserRoleDTO { UserID = 1, RoleID = 1 },  // Usando UserID y RoleID como en tu DTO
                new UserRoleDTO { UserID = 2, RoleID = 2 }
            };

            // Configurar mocks
            _mockUserRoleRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(userRoleEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<UserRoleDTO>>(It.IsAny<IEnumerable<UserRole>>())).Returns(userRoleDtos);

            // Act
            var result = await _userRoleService.GetAllUserRolesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserRoleDTO>>(result);
            Assert.Equal(2, result.Count());
        }

        

        [Fact]
        public async Task AddUserRoleAsync_ShouldAddUserRoleSuccessfully()
        {
            // Arrange
            var userRoleDto = new UserRoleDTO { UserID = 1, RoleID = 1 }; // Propiedades corregidas
            var userRoleEntity = new UserRole { UserId = 1, RoleId = 1 }; // Usando UserId y RoleId como en la entidad

            // Configurar mocks
            _mockMapper.Setup(m => m.Map<UserRole>(It.IsAny<UserRoleDTO>())).Returns(userRoleEntity);

            // Act
            await _userRoleService.AddUserRoleAsync(userRoleDto);

            // Assert
            _mockUserRoleRepository.Verify(repo => repo.AddAsync(It.IsAny<UserRole>()), Times.Once);
            _mockUserRoleRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }


        public async Task DeleteUserRoleAsync_ShouldDeleteUserRoleSuccessfully()
        {
            // Arrange
            var userRoleDto = new UserRoleDTO { UserID = 1, RoleID = 1 }; // Propiedades corregidas
            var userRoleEntity = new UserRole { UserId = 1, RoleId = 1 }; // Usando UserId y RoleId como en la entidad

            // Configurar mocks
            _mockMapper.Setup(m => m.Map<UserRole>(It.IsAny<UserRoleDTO>())).Returns(userRoleEntity);

            // Act
            await _userRoleService.DeleteUserRoleAsync(userRoleDto);

            // Assert
            _mockUserRoleRepository.Verify(repo => repo.Delete(It.IsAny<UserRole>()), Times.Once);
            _mockUserRoleRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

    }
}
