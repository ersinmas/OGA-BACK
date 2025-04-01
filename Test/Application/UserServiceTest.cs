using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _configMock = new Mock<IConfiguration>();

        _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object, _configMock.Object);
    }

    [Fact]
    public async Task GetAllUsersAsync_ReturnsEnabledUsers()
    {
        var users = new List<User>
        {
            new User { UserId = 1, Email = "user1@example.com", Enabled = true },
            new User { UserId = 2, Email = "user2@example.com", Enabled = false }
        };

        _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);
        _mapperMock.Setup(m => m.Map<IEnumerable<UserDTO>>(It.IsAny<IEnumerable<User>>()))
            .Returns((IEnumerable<User> u) => u.Where(x => x.Enabled).Select(x => new UserDTO { UserId = x.UserId, Email = x.Email }));

        var result = await _userService.GetAllUsersAsync();

        Assert.Single(result);
        Assert.Equal("user1@example.com", result.First().Email);
    }

    [Fact]
    public async Task AddUserAsync_AddsUserSuccessfully()
    {
        var userDto = new UserDTO { UserId = 1, Email = "test@example.com" };
        var user = new User { UserId = 1, Email = "test@example.com" };

        _mapperMock.Setup(m => m.Map<User>(It.IsAny<UserDTO>())).Returns(user);
        _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _userRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);

        await _userService.AddUserAsync(userDto);

        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteUserAsync_DeletesUser()
    {
        var user = new User { UserId = 1, Email = "delete@example.com" };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);
        _userRepositoryMock.Setup(repo => repo.Delete(It.IsAny<User>()));
        _userRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);

        await _userService.DeleteUserAsync(1);

        _userRepositoryMock.Verify(repo => repo.Delete(It.IsAny<User>()), Times.Once);
        _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }
}
