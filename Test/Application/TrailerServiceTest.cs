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
    public class TrailerServiceTests
    {
        private readonly Mock<ITrailerRepository> _mockTrailerRepository;
        private readonly Mock<IVehicleTrailerService> _mockVehicleTrailerService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TrailerService _trailerService;

        public TrailerServiceTests()
        {
            _mockTrailerRepository = new Mock<ITrailerRepository>();
            _mockVehicleTrailerService = new Mock<IVehicleTrailerService>();
            _mockMapper = new Mock<IMapper>();
            _trailerService = new TrailerService(_mockTrailerRepository.Object, _mockMapper.Object, _mockVehicleTrailerService.Object);
        }

        [Fact]
        public async Task GetAllTrailersAsync_ShouldReturnListOfTrailers()
        {
            // Arrange
            var trailerEntities = new List<Trailer>
            {
                new Trailer { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000, Enabled = true },
                new Trailer { TrailerId = 2, RegNumber = 67890, RegDate = DateTime.Now, MaxWeight = 2000, Enabled = true }
            };
            var trailerDtos = new List<TrailerDTO>
            {
                new TrailerDTO { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000 },
                new TrailerDTO { TrailerId = 2, RegNumber = 67890, RegDate = DateTime.Now, MaxWeight = 2000 }
            };

            _mockVehicleTrailerService.Setup(service => service.CheckExpiredAssignmentsAsync()).Returns(Task.CompletedTask);
            _mockTrailerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(trailerEntities);
            _mockMapper.Setup(m => m.Map<IEnumerable<TrailerDTO>>(It.IsAny<IEnumerable<Trailer>>())).Returns(trailerDtos);

            // Act
            var result = await _trailerService.GetAllTrailersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TrailerDTO>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTrailerByIdAsync_ShouldReturnTrailer_WhenTrailerExists()
        {
            // Arrange
            var trailerEntity = new Trailer { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000 };
            var trailerDto = new TrailerDTO { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000 };
            _mockTrailerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(trailerEntity);
            _mockMapper.Setup(m => m.Map<TrailerDTO>(It.IsAny<Trailer>())).Returns(trailerDto);

            // Act
            var result = await _trailerService.GetTrailerByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TrailerId);
            Assert.Equal(12345, result.RegNumber);
        }

        [Fact]
        public async Task GetTrailerByIdAsync_ShouldReturnNull_WhenTrailerDoesNotExist()
        {
            // Arrange
            _mockTrailerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Trailer)null);

            // Act
            var result = await _trailerService.GetTrailerByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddTrailerAsync_ShouldAddTrailerSuccessfully()
        {
            // Arrange
            var trailerDto = new TrailerDTO { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000 };
            var trailerEntity = new Trailer { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000 };
            _mockMapper.Setup(m => m.Map<Trailer>(It.IsAny<TrailerDTO>())).Returns(trailerEntity);

            // Act
            await _trailerService.AddTrailerAsync(trailerDto);

            // Assert
            _mockTrailerRepository.Verify(repo => repo.AddAsync(It.IsAny<Trailer>()), Times.Once);
            _mockTrailerRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateTrailerAsync_ShouldUpdateTrailerSuccessfully()
        {
            // Arrange
            var trailerDto = new TrailerDTO { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000 };
            var trailerEntity = new Trailer { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000 };
            _mockMapper.Setup(m => m.Map<Trailer>(It.IsAny<TrailerDTO>())).Returns(trailerEntity);

            // Act
            await _trailerService.UpdateTrailerAsync(trailerDto);

            // Assert
            _mockTrailerRepository.Verify(repo => repo.Update(It.IsAny<Trailer>()), Times.Once);
            _mockTrailerRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteTrailerAsync_ShouldDeleteTrailer_WhenTrailerExists()
        {
            // Arrange
            var trailerEntity = new Trailer { TrailerId = 1, RegNumber = 12345, RegDate = DateTime.Now, MaxWeight = 1000 };
            _mockTrailerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(trailerEntity);

            // Act
            await _trailerService.DeleteTrailerAsync(1);

            // Assert
            _mockTrailerRepository.Verify(repo => repo.Delete(It.IsAny<Trailer>()), Times.Once);
            _mockTrailerRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteTrailerAsync_ShouldNotDeleteTrailer_WhenTrailerDoesNotExist()
        {
            // Arrange
            _mockTrailerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Trailer)null);

            // Act
            await _trailerService.DeleteTrailerAsync(1);

            // Assert
            _mockTrailerRepository.Verify(repo => repo.Delete(It.IsAny<Trailer>()), Times.Never);
            _mockTrailerRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }
    }
}
