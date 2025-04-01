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

namespace Application.Tests
{
    public class RoleServiceTests
    {
        private readonly Mock<IRoleRepository> _mockRoleRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RoleService _roleService;

        public RoleServiceTests()
        {
            _mockRoleRepository = new Mock<IRoleRepository>();
            _mockMapper = new Mock<IMapper>();
            _roleService = new RoleService(_mockRoleRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllRolesAsync_ShouldReturnListOfRoles()
        {
            // Arrange
            var roleEntities = new List<Role> { new Role { RoleId = 1, Name = "Admin", Description = "Administrator role" } };
            var roleDtos = new List<RoleDTO> { new RoleDTO { RoleId = 1, Name = "Admin", Description = "Administrator role" } };
            _mockRoleRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(roleEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<RoleDTO>>(It.IsAny<IEnumerable<Role>>())).Returns(roleDtos);

            // Act
            var result = await _roleService.GetAllRolesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<RoleDTO>>(result);
            Assert.Single(result);
            Assert.Equal("Admin", result.First().Name);
            Assert.Equal("Administrator role", result.First().Description);
        }

        [Fact]
        public async Task GetRoleByIdAsync_ShouldReturnRole_WhenRoleExists()
        {
            // Arrange
            var roleEntity = new Role { RoleId = 1, Name = "Admin", Description = "Administrator role" };
            var roleDto = new RoleDTO { RoleId = 1, Name = "Admin", Description = "Administrator role" };
            _mockRoleRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(roleEntity);
            _mockMapper.Setup(m => m.Map<RoleDTO>(It.IsAny<Role>())).Returns(roleDto);

            // Act
            var result = await _roleService.GetRoleByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.RoleId);
            Assert.Equal("Admin", result.Name);
            Assert.Equal("Administrator role", result.Description);
        }

        [Fact]
        public async Task GetRoleByIdAsync_ShouldReturnNull_WhenRoleDoesNotExist()
        {
            // Arrange
            _mockRoleRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Role)null);

            // Act
            var result = await _roleService.GetRoleByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddRoleAsync_ShouldAddRoleSuccessfully()
        {
            // Arrange
            var roleDto = new RoleDTO { RoleId = 1, Name = "Admin", Description = "Administrator role" };
            var roleEntity = new Role { RoleId = 1, Name = "Admin", Description = "Administrator role" };
            _mockMapper.Setup(m => m.Map<Role>(It.IsAny<RoleDTO>())).Returns(roleEntity);

            // Act
            await _roleService.AddRoleAsync(roleDto);

            // Assert
            _mockRoleRepository.Verify(repo => repo.AddAsync(It.IsAny<Role>()), Times.Once);
            _mockRoleRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateRoleAsync_ShouldUpdateRoleSuccessfully()
        {
            // Arrange
            var roleDto = new RoleDTO { RoleId = 1, Name = "Admin", Description = "Administrator role" };
            var roleEntity = new Role { RoleId = 1, Name = "Admin", Description = "Administrator role" };
            _mockMapper.Setup(m => m.Map<Role>(It.IsAny<RoleDTO>())).Returns(roleEntity);

            // Act
            await _roleService.UpdateRoleAsync(roleDto);

            // Assert
            _mockRoleRepository.Verify(repo => repo.Update(It.IsAny<Role>()), Times.Once);
            _mockRoleRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteRoleAsync_ShouldDeleteRole_WhenRoleExists()
        {
            // Arrange
            var roleEntity = new Role { RoleId = 1, Name = "Admin", Description = "Administrator role" };
            _mockRoleRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(roleEntity);

            // Act
            await _roleService.DeleteRoleAsync(1);

            // Assert
            _mockRoleRepository.Verify(repo => repo.Delete(It.IsAny<Role>()), Times.Once);
            _mockRoleRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteRoleAsync_ShouldNotDeleteRole_WhenRoleDoesNotExist()
        {
            // Arrange
            _mockRoleRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Role)null);

            // Act
            await _roleService.DeleteRoleAsync(1);

            // Assert
            _mockRoleRepository.Verify(repo => repo.Delete(It.IsAny<Role>()), Times.Never);
            _mockRoleRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }
    }
}
