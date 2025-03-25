using Microsoft.Extensions.Logging;
using Moq;
using Thunders.TechTest.ApiService.App.Consumer;
using Thunders.TechTest.ApiService.App.Contracts.Enums;
using Thunders.TechTest.ApiService.App.Contracts.Messages;
using Thunders.TechTest.ApiService.App.Services;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Tests.Consumers
{
    public class ReportConsumerTests
    {
        private readonly Mock<ILogger<CreateReportService>> _loggerMock;
        private readonly Mock<ICreateVehicleCountByTollPlazaService> _createVehicleCountByTollPlazaServiceMock;
        private readonly Mock<ICreateHourlyRevenueService> _createHourlyRevenueServiceMock;
        private readonly Mock<ICreateTopEarningTollPlazaService> _createTopEarningTollPlazaServiceMock;
        private readonly ReportConsumer _consumer;

        public ReportConsumerTests()
        {
            _loggerMock = new Mock<ILogger<CreateReportService>>();
            _createVehicleCountByTollPlazaServiceMock = new Mock<ICreateVehicleCountByTollPlazaService>();
            _createHourlyRevenueServiceMock = new Mock<ICreateHourlyRevenueService>();
            _createTopEarningTollPlazaServiceMock = new Mock<ICreateTopEarningTollPlazaService>();
            _consumer = new ReportConsumer(
                _loggerMock.Object,
                _createVehicleCountByTollPlazaServiceMock.Object,
                _createHourlyRevenueServiceMock.Object,
                _createTopEarningTollPlazaServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCallCreateVehicleCountByTollPlazaService_WhenReportTypeIsVehicleCountByTollPlaza()
        {
            // Arrange
            var message = new CreateReportMessage { Id = Guid.NewGuid(), Type = ReportType.VehicleCountByTollPlaza };
            _createVehicleCountByTollPlazaServiceMock.Setup(s => s.Execute(message.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<Guid>.Ok(message.Id));

            // Act
            await _consumer.Handle(message);

            // Assert
            _createVehicleCountByTollPlazaServiceMock.Verify(s => s.Execute(message.Id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallCreateHourlyRevenueService_WhenReportTypeIsHourlyRevenueByCity()
        {
            // Arrange
            var message = new CreateReportMessage { Id = Guid.NewGuid(), Type = ReportType.HourlyRevenueByCity };
            _createHourlyRevenueServiceMock.Setup(s => s.Execute(message.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<Guid>.Ok(message.Id));

            // Act
            await _consumer.Handle(message);

            // Assert
            _createHourlyRevenueServiceMock.Verify(s => s.Execute(message.Id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallCreateTopEarningTollPlazaService_WhenReportTypeIsTopEarningTollPlaza()
        {
            // Arrange
            var message = new CreateReportMessage { Id = Guid.NewGuid(), Type = ReportType.TopEarningTollPlaza };
            _createTopEarningTollPlazaServiceMock.Setup(s => s.Execute(message.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<Guid>.Ok(message.Id));

            // Act
            await _consumer.Handle(message);

            // Assert
            _createTopEarningTollPlazaServiceMock.Verify(s => s.Execute(message.Id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
