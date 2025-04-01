using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class VehicleTypeServiceTests
{
    private readonly Mock<IVehicleTypeRepository> _vehicleTypeRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly VehicleTypeService _service;

    public VehicleTypeServiceTests()
    {
        _vehicleTypeRepositoryMock = new Mock<IVehicleTypeRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new VehicleTypeService(_vehicleTypeRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllVehicleTypeAsync_ReturnsVehicleTypes()
    {
        // Arrange
        var vehicleTypes = new List<VehicleType>
        {
            new VehicleType { VehicleTypeId = 1, Name = "Car" },
            new VehicleType { VehicleTypeId = 2, Name = "Truck" }
        };

        _vehicleTypeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(vehicleTypes);
        _mapperMock.Setup(m => m.Map<IEnumerable<VehicleTypeDTO>>(vehicleTypes))
                   .Returns(vehicleTypes.Select(x => new VehicleTypeDTO { VehicleTypeId = x.VehicleTypeId, Name = x.Name }));

        // Act
        var result = await _service.GetAllVehicleTypeAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, v => v.Name == "Car");
        Assert.Contains(result, v => v.Name == "Truck");
    }

    [Fact]
    public async Task GetVehicleTypeByIdAsync_ReturnsVehicleType()
    {
        // Arrange
        var vehicleType = new VehicleType { VehicleTypeId = 1, Name = "Car" };
        _vehicleTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(vehicleType);
        _mapperMock.Setup(m => m.Map<VehicleTypeDTO>(vehicleType))
                   .Returns(new VehicleTypeDTO { VehicleTypeId = vehicleType.VehicleTypeId, Name = vehicleType.Name });

        // Act
        var result = await _service.GetVehicleTypeByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Car", result.Name);
    }

    [Fact]
    public async Task AddVehicleTypeAsync_AddsVehicleType()
    {
        // Arrange
        var vehicleTypeDto = new VehicleTypeDTO { Name = "Bike" };
        var vehicleType = new VehicleType { VehicleTypeId = 0, Name = "Bike" };  // ID will be set by DB

        _mapperMock.Setup(m => m.Map<VehicleType>(vehicleTypeDto)).Returns(vehicleType);

        // Configurar correctamente el retorno de SaveChangesAsync para devolver un Task<int>
        _vehicleTypeRepositoryMock.Setup(repo => repo.AddAsync(vehicleType)).Returns(Task.CompletedTask);
        _vehicleTypeRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);  // Esto debe ser un Task<int>

        // Act
        await _service.AddVehicleTypeAsync(vehicleTypeDto);

        // Assert
        _vehicleTypeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<VehicleType>()), Times.Once);
        _vehicleTypeRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }


    [Fact]
    public async Task UpdateVehicleTypeAsync_UpdatesVehicleType()
    {
        // Arrange
        var vehicleTypeDto = new VehicleTypeDTO { VehicleTypeId = 1, Name = "Updated Car" };
        var vehicleType = new VehicleType { VehicleTypeId = 1, Name = "Car" };

        // Configura el mapeo del DTO al objeto VehicleType
        _mapperMock.Setup(m => m.Map<VehicleType>(vehicleTypeDto)).Returns(vehicleType);

        // Configura el repositorio para la actualización
        _vehicleTypeRepositoryMock.Setup(repo => repo.Update(vehicleType)).Verifiable();

        // Configura SaveChangesAsync para devolver un Task<int>
        _vehicleTypeRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);  // Cambiado para devolver un Task<int>

        // Act
        await _service.UpdateVehicleTypeAsync(vehicleTypeDto);

        // Assert
        _vehicleTypeRepositoryMock.Verify(repo => repo.Update(It.IsAny<VehicleType>()), Times.Once);
        _vehicleTypeRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }


    [Fact]
    
    public async Task DeleteVehicleTypeAsync_DeletesVehicleType()
    {
        // Arrange
        var vehicleType = new VehicleType { VehicleTypeId = 1, Name = "Car" };

        // Configura el repositorio para devolver el objeto correctamente
        _vehicleTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(vehicleType);

        // Configura la eliminación y los cambios en el repositorio
        _vehicleTypeRepositoryMock.Setup(repo => repo.Delete(vehicleType)).Verifiable();
        _vehicleTypeRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);  // Cambiado para devolver un Task<int>

        // Act
        await _service.DeleteVehicleTypeAsync(1);

        // Assert
        _vehicleTypeRepositoryMock.Verify(repo => repo.Delete(It.IsAny<VehicleType>()), Times.Once);
        _vehicleTypeRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }


    [Fact]
    public async Task GetVehicleTypeByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        _vehicleTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((VehicleType)null);

        // Act
        var result = await _service.GetVehicleTypeByIdAsync(99);

        // Assert
        Assert.Null(result);
    }
}
