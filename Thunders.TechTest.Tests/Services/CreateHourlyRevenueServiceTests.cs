using Microsoft.Extensions.Logging;
using Moq;
using Thunders.TechTest.ApiService.App.Services;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Infra.Repositories.Dtos;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Tests.Services
{
    public class CreateHourlyRevenueServiceTests
    {
        private readonly Mock<ILogger<CreateHourlyRevenueService>> _loggerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateHourlyRevenueService _service;

        public CreateHourlyRevenueServiceTests()
        {
            _loggerMock = new Mock<ILogger<CreateHourlyRevenueService>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _service = new CreateHourlyRevenueService(_loggerMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnOk_WhenIdempotenceIsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var hourlyRevenueByCity = new HourlyRevenueByCity();
            _unitOfWorkMock.Setup(u => u.HourlyRevenueByCityRepository.GetByReportIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<HourlyRevenueByCity?>.Ok(hourlyRevenueByCity));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(id, result.Data.Value);
        }

        [Fact]
        public async Task Execute_ShouldReturnFail_WhenGetByReportIdAsyncFails()
        {
            // Arrange
            var id = Guid.NewGuid();
            var errors = new List<Error> { new Error("Error", "Error message") };
            _unitOfWorkMock.Setup(u => u.HourlyRevenueByCityRepository.GetByReportIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<HourlyRevenueByCity?>.Fail(errors));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }

        [Fact]
        public async Task Execute_ShouldReturnFail_WhenGetTotalAmountByHourAndCityAsyncFails()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.HourlyRevenueByCityRepository.GetByReportIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<HourlyRevenueByCity?>.Ok(null));
            var errors = new List<Error> { new Error("Error", "Error message") };
            _unitOfWorkMock.Setup(u => u.TollTransactionRepository.GetTotalAmountByHourAndCityAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<List<HourlyRevenueByCityDto>>.Fail(errors));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }

        [Fact]
        public async Task Execute_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.HourlyRevenueByCityRepository.GetByReportIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<HourlyRevenueByCity?>.Ok(null));
            var hourlyRevenueByCityDtos = new List<HourlyRevenueByCityDto>
            {
                new HourlyRevenueByCityDto { City = "City1", Hour = 1, TotalAmount = 100 },
                new HourlyRevenueByCityDto { City = "City2", Hour = 2, TotalAmount = 200 }
            };
            _unitOfWorkMock.Setup(u => u.TollTransactionRepository.GetTotalAmountByHourAndCityAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<List<HourlyRevenueByCityDto>>.Ok(hourlyRevenueByCityDtos));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(id, result.Data.Value);
            _unitOfWorkMock.Verify(u => u.HourlyRevenueByCityRepository.InsertAsync(It.IsAny<HourlyRevenueByCity>(), It.IsAny<CancellationToken>()), Times.Exactly(hourlyRevenueByCityDtos.Count));
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldReturnFail_WhenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.HourlyRevenueByCityRepository.GetByReportIdAsync(id, It.IsAny<CancellationToken>()))
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
