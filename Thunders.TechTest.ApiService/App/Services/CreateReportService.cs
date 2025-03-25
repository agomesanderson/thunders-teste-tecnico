using Thunders.TechTest.ApiService.App.Contracts.Enums;
using Thunders.TechTest.ApiService.App.Contracts.Messages;
using Thunders.TechTest.ApiService.App.Contracts.Requests;
using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.App.Services.Errors;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Shared;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.App.Services
{
    public class CreateReportService : ICreateReportService
    {
        private readonly ILogger<CreateReportService> _logger;
        private readonly IMessageSender _bus;

        public CreateReportService(
            ILogger<CreateReportService> logger,
            IMessageSender bus
        )
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task<Result<CreateReportResponse>> Execute(
            Guid id,
            CreateReportRequest request,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                _logger.LogInformation("CreateReportService started");

                await _bus.SendLocal(new CreateReportMessage { Id = id, Type = request.Type });

                _logger.LogInformation("CreateReportService finished");

                return Result<CreateReportResponse>.Ok(new CreateReportResponse { Id = id, Type = request.Type });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateReportService failed");

                return Result<CreateReportResponse>.Fail(
                    new UnexpectedError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
