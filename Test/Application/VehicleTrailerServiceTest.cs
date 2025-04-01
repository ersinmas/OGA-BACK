using Application.DTOs;
using Application.Interfaces;
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

public class VehicleTrailerServiceTests
{
    private readonly Mock<IVehicleTrailerRepository> _vehicleTrailerRepositoryMock;
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly Mock<ITrailerRepository> _trailerRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly VehicleTrailerService _service;

    public VehicleTrailerServiceTests()
    {
        _vehicleTrailerRepositoryMock = new Mock<IVehicleTrailerRepository>();
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _trailerRepositoryMock = new Mock<ITrailerRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new VehicleTrailerService(
            _vehicleTrailerRepositoryMock.Object,
            _mapperMock.Object,
            _vehicleRepositoryMock.Object,
            _trailerRepositoryMock.Object
        );
    }

    [Fact]
    public async Task AddVehicleTrailerAsync_SuccessfullyAddsTrailer()
    {
        var vehicleTrailerDto = new VehicleTrailerDTO { VehicleId = 1, TrailerId = 1 };
        var vehicle = new Vehicles { VehicleId = 1, Enabled = true };
        var trailer = new Trailer { TrailerId = 1, Enabled = true };

        _mapperMock.Setup(m => m.Map<VehicleTrailer>(It.IsAny<VehicleTrailerDTO>()))
            .Returns(new VehicleTrailer { VehicleId = 1, TrailerId = 1 });

        _vehicleRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(vehicle);
        _trailerRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(trailer);
        _vehicleTrailerRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<VehicleTrailer>()))
            .Returns(Task.CompletedTask);
        _trailerRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);
        _vehicleTrailerRepositoryMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);

        await _service.AddVehicleTrailerAsync(vehicleTrailerDto);

        _trailerRepositoryMock.Verify(repo => repo.Update(It.IsAny<Trailer>()), Times.Once);
        _vehicleTrailerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<VehicleTrailer>()), Times.Once);
    }

    
    
}

