using Microsoft.Extensions.Logging;
using Moq;
using Thunders.TechTest.ApiService.App.Contracts.Requests;
using Thunders.TechTest.ApiService.App.Services;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enums;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Tests.Services
{
    public class CreateTollTransactionServiceTests
    {
        private readonly Mock<ILogger<CreateTollTransactionService>> _loggerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateTollTransactionService _service;

        public CreateTollTransactionServiceTests()
        {
            _loggerMock = new Mock<ILogger<CreateTollTransactionService>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _service = new CreateTollTransactionService(_loggerMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnOk_WhenIdempotenceIsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var tollTransaction = new TollTransaction();
            _unitOfWorkMock.Setup(u => u.TollTransactionRepository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<TollTransaction?>.Ok(tollTransaction));

            var request = new CreateTollTransactionRequest
            {
                UsageTime = DateTime.UtcNow,
                TollPlaza = "Plaza1",
                City = "City1",
                State = FederationUnit.SP,
                AmountPaid = 100,
                VehicleType = VehicleType.Car
            };

            // Act
            var result = await _service.Execute(id, request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(id, result.Data.Value.Id);
        }

        [Fact]
        public async Task Execute_ShouldReturnFail_WhenGetByIdAsyncFails()
        {
            // Arrange
            var id = Guid.NewGuid();
            var errors = new List<Error> { new Error("Error", "Error message") };
            _unitOfWorkMock.Setup(u => u.TollTransactionRepository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<TollTransaction?>.Fail(errors));

            var request = new CreateTollTransactionRequest
            {
                UsageTime = DateTime.UtcNow,
                TollPlaza = "Plaza1",
                City = "City1",
                State = FederationUnit.SP,
                AmountPaid = 100,
                VehicleType = VehicleType.Car
            };

            // Act
            var result = await _service.Execute(id, request);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(errors, result.Errors);
        }

        [Fact]
        public async Task Execute_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.TollTransactionRepository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<TollTransaction?>.Ok(null));

            var request = new CreateTollTransactionRequest
            {
                UsageTime = DateTime.UtcNow,
                TollPlaza = "Plaza1",
                City = "City1",
                State = FederationUnit.SP,
                AmountPaid = 100,
                VehicleType = VehicleType.Car
            };

            // Act
            var result = await _service.Execute(id, request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(id, result.Data.Value.Id);
            _unitOfWorkMock.Verify(u => u.TollTransactionRepository.InsertAsync(It.IsAny<TollTransaction>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldReturnFail_WhenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(u => u.TollTransactionRepository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            var request = new CreateTollTransactionRequest
            {
                UsageTime = DateTime.UtcNow,
                TollPlaza = "Plaza1",
                City = "City1",
                State = FederationUnit.SP,
                AmountPaid = 100,
                VehicleType = VehicleType.Car
            };

            // Act
            var result = await _service.Execute(id, request);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal("Test exception", result.Errors[0].Message);
        }
    }
}

