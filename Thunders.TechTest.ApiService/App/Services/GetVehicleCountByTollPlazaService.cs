using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.App.Services.Errors;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services
{
    public class GetVehicleCountByTollPlazaService : IGetVehicleCountByTollPlazaService
    {
        private readonly ILogger<GetVehicleCountByTollPlazaService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetVehicleCountByTollPlazaService(
            ILogger<GetVehicleCountByTollPlazaService> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<GetVehicleCountByTollPlazaResponse>>> Execute(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                _logger.LogInformation("GetVehicleCountByTollPlazaService started");

                var vehicleCountByTollPlazaReportResult = await _unitOfWork.VehicleCountByTollPlazaRepository.GetReportAsync(id, cancellationToken);

                if (vehicleCountByTollPlazaReportResult.IsFailure)
                    return Result<List<GetVehicleCountByTollPlazaResponse>>.Fail(vehicleCountByTollPlazaReportResult.Errors);

                var hasData = vehicleCountByTollPlazaReportResult.Data.Match(
                  some:
                    value => value.Count > 0,
                  none:
                    () => false
                );

                if (!hasData)
                    return Result<List<GetVehicleCountByTollPlazaResponse>>.Fail(new NotFoundError("Report not found"));

                var dataResult = vehicleCountByTollPlazaReportResult.Data!.Value;

                var report = dataResult.Select(x => 
                    new GetVehicleCountByTollPlazaResponse { 
                        TollPlaza = x.TollPlaza, 
                        VehicleType = x.VehicleType, 
                        Count = x.VehicleCount
                    }).ToList();

                return Result<List<GetVehicleCountByTollPlazaResponse>>.Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetVehicleCountByTollPlazaService failed");

                return Result<List<GetVehicleCountByTollPlazaResponse>>.Fail(
                    new UnexpectedError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
