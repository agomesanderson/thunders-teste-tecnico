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

                switch (message.Type)
                {
                    case ReportType.VehicleCountByTollPlaza:
                        var createVehicleCountByTollPlazaServiceResult = await _createVehicleCountByTollPlazaService.Execute(message.Id);

                        if (createVehicleCountByTollPlazaServiceResult.IsFailure)
                        {
                            var errors = string.Join("\n", createVehicleCountByTollPlazaServiceResult.Errors);
                            _logger.LogError(errors);
                            throw new Exception(errors);
                        }

                        break;

                    case ReportType.TopEarningTollPlaza:
                        var createTopEarningTollPlazaResult = await _createTopEarningTollPlazaService.Execute(message.Id);

                        if (createTopEarningTollPlazaResult.IsFailure)
                        {
                            var errors = string.Join("\n", createTopEarningTollPlazaResult.Errors);
                            _logger.LogError(errors);
                            throw new Exception(errors);
                        }
                        break;

                    case ReportType.HourlyRevenueByCity:
                        var createHourlyRevenueByCityResult = await _createHourlyRevenueService.Execute(message.Id);

                        if (createHourlyRevenueByCityResult.IsFailure)
                        {
                            var errors = string.Join("\n", createHourlyRevenueByCityResult.Errors);
                            _logger.LogError(errors);
                            throw new Exception(errors);
                        }
                        break;
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
