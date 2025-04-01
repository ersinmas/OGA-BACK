using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

public class VehicleServiceTests
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly VehicleService _vehicleService;

    public VehicleServiceTests()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _mapperMock = new Mock<IMapper>();
        _vehicleService = new VehicleService(_vehicleRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllVehiclesAsync_ReturnsMappedVehicleDTOs()
    {
        var vehicles = new List<Vehicles>
        {
            new Vehicles { VehicleId = 1, VehicleTypeId = 2, RegNumber = 12345, RegDate = DateTime.UtcNow }
        };
        var vehicleDTOs = new List<VehicleDTO>
        {
            new VehicleDTO { VehicleId = 1, VehicleTypeId = 2, RegNumber = 12345, RegDate = DateTime.UtcNow }
        };

        _vehicleRepositoryMock.Setup(repo => repo.GetVehiclesWithTypeAsync()).ReturnsAsync(vehicles);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<VehicleDTO>>(vehicles)).Returns(vehicleDTOs);

        var result = await _vehicleService.GetAllVehiclesAsync();

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetVehicleByIdAsync_VehicleExists_ReturnsMappedVehicleDTO()
    {
        var vehicle = new Vehicles { VehicleId = 1, VehicleTypeId = 2, RegNumber = 12345, RegDate = DateTime.UtcNow };
        var vehicleDTO = new VehicleDTO { VehicleId = 1, VehicleTypeId = 2, RegNumber = 12345, RegDate = DateTime.UtcNow };

        _vehicleRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(vehicle);
        _mapperMock.Setup(mapper => mapper.Map<VehicleDTO>(vehicle)).Returns(vehicleDTO);

        var result = await _vehicleService.GetVehicleByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.VehicleId);
    }

    [Fact]
    public async Task AddVehicleAsync_ValidVehicleDTO_CallsRepositoryAndSavesChanges()
    {
        var vehicleDTO = new VehicleDTO { VehicleId = 1, VehicleTypeId = 2, RegNumber = 12345, RegDate = DateTime.UtcNow };
        var vehicle = new Vehicles { VehicleId = 1, VehicleTypeId = 2, RegNumber = 12345, RegDate = DateTime.UtcNow };

        _mapperMock.Setup(mapper => mapper.Map<Vehicles>(vehicleDTO)).Returns(vehicle);

        await _vehicleService.AddVehicleAsync(vehicleDTO);

        _vehicleRepositoryMock.Verify(repo => repo.AddAsync(vehicle), Times.Once);
        _vehicleRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteVehicleAsync_ExistingVehicle_CallsDeleteAndSavesChanges()
    {
        var vehicle = new Vehicles { VehicleId = 1 };

        _vehicleRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(vehicle);

        await _vehicleService.DeleteVehicleAsync(1);

        _vehicleRepositoryMock.Verify(repo => repo.Delete(vehicle), Times.Once);
        _vehicleRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }
}
