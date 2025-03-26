using Rebus.Handlers;
using Thunders.TechTest.ApiService.App.Contracts.Enums;
using Thunders.TechTest.ApiService.App.Contracts.Messages;
using Thunders.TechTest.ApiService.App.Services;
using Thunders.TechTest.ApiService.App.Services.Interfaces;

namespace Thunders.TechTest.ApiService.App.Consumer
{
    public class ReportConsumer : IHandleMessages<CreateReportMessage>
    {
        private readonly ILogger<CreateReportService> _logger;
        private readonly ICreateVehicleCountByTollPlazaService _createVehicleCountByTollPlazaService;
        private readonly ICreateHourlyRevenueService _createHourlyRevenueService;
        private readonly ICreateTopEarningTollPlazaService _createTopEarningTollPlazaService;

        public ReportConsumer(
            ILogger<CreateReportService> logger,
            ICreateVehicleCountByTollPlazaService createVehicleCountByTollPlazaService,
            ICreateHourlyRevenueService createHourlyRevenueService,
            ICreateTopEarningTollPlazaService createTopEarningTollPlazaService
        )
        {
            _logger = logger;
            _createVehicleCountByTollPlazaService = createVehicleCountByTollPlazaService;
            _createHourlyRevenueService = createHourlyRevenueService;
            _createTopEarningTollPlazaService = createTopEarningTollPlazaService;
        }

        public async Task Handle(CreateReportMessage message)
        {
            try
            {
                _logger.LogInformation("ReportConsumer started");
                _logger.LogInformation($"Message received: {message}");

                var result = message.Type switch
                {
                    ReportType.VehicleCountByTollPlaza => await _createVehicleCountByTollPlazaService.Execute(message.Id),
                    ReportType.TopEarningTollPlaza => await _createTopEarningTollPlazaService.Execute(message.Id),
                    ReportType.HourlyRevenueByCity => await _createHourlyRevenueService.Execute(message.Id),
                    _ => throw new ArgumentOutOfRangeException(nameof(message.Type), $"Unsupported report type: {message.Type}")
                };

                if (result.IsFailure)
                {
                    var errors = string.Join("\n", result.Errors);
                    _logger.LogError(errors);
                    throw new Exception(errors);
                }

                _logger.LogInformation("ReportConsumer finished");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ReportConsumer failed");
                throw new Exception("ReportConsumer failed", ex);
            }
        }
    }
}
