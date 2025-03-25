using Microsoft.Extensions.Logging;
using Moq;
using Thunders.TechTest.ApiService.App.Services;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enums;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Tests.Services
{
    public class GetVehicleCountByTollPlazaServiceTests
    {
        private readonly Mock<ILogger<GetVehicleCountByTollPlazaService>> _loggerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetVehicleCountByTollPlazaService _service;

        public GetVehicleCountByTollPlazaServiceTests()
        {
            _loggerMock = new Mock<ILogger<GetVehicleCountByTollPlazaService>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _service = new GetVehicleCountByTollPlazaService(_loggerMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnOk_WhenDataExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var vehicleCountByTollPlazaList = new List<VehicleCountByTollPlaza>();
            vehicleCountByTollPlazaList.Add(VehicleCountByTollPlaza.Create("Plaza1", VehicleType.Car, 100, Guid.NewGuid()));
            vehicleCountByTollPlazaList.Add(VehicleCountByTollPlaza.Create("Plaza2", VehicleType.Motorcycle, 200, Guid.NewGuid()));
            
            _unitOfWorkMock.Setup(u => u.VehicleCountByTollPlazaRepository.GetReportAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<List<VehicleCountByTollPlaza>>.Ok(vehicleCountByTollPlazaList));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Value.Count);
            Assert.Equal("Plaza1", result.Data.Value[0].TollPlaza);
            Assert.Equal(VehicleType.Car, result.Data.Value[0].VehicleType);
            Assert.Equal(100, result.Data.Value[0].Count);
        }

        [Fact]
        public async Task Execute_ShouldReturnFail_WhenGetReportAsyncFails()
        {
            // Arrange
            var id = Guid.NewGuid();
            var errors = new List<Error> { new Error("Error", "Error message") };
            _unitOfWorkMock.Setup(u => u.VehicleCountByTollPlazaRepository.GetReportAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<List<VehicleCountByTollPlaza>>.Fail(errors));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }

        [Fact]
        public async Task Execute_ShouldReturnFail_WhenNoDataExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.VehicleCountByTollPlazaRepository.GetReportAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<List<VehicleCountByTollPlaza>>.Ok(new List<VehicleCountByTollPlaza>()));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal("Report not found", result.Errors[0].Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnFail_WhenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.VehicleCountByTollPlazaRepository.GetReportAsync(id, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal("Test exception", result.Errors[0].Message);
        }
    }
}
