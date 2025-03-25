using Thunders.TechTest.ApiService.App.Services.Errors;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services
{
    public class CreateVehicleCountByTollPlazaService : ICreateVehicleCountByTollPlazaService
    {
        private readonly ILogger<CreateVehicleCountByTollPlazaService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVehicleCountByTollPlazaService(
            ILogger<CreateVehicleCountByTollPlazaService> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Execute(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                _logger.LogInformation("CreateVehicleCountByTollPlazaService started");

                var vehicleCountByTollPlazaReportResult = await _unitOfWork.VehicleCountByTollPlazaRepository.GetByReportIdAsync(id, cancellationToken);

                if (vehicleCountByTollPlazaReportResult.IsFailure)
                    return Result<Guid>.Fail(vehicleCountByTollPlazaReportResult.Errors);

                var idempotence = vehicleCountByTollPlazaReportResult.Data.HasValue;

                if (idempotence)
                    return Result<Guid>.Ok(id);

                var vehicleCountByTollPlazaResult = await _unitOfWork.TollTransactionRepository.GetVehicleTypeCountByTollPlazaAsync(cancellationToken);

                if (vehicleCountByTollPlazaResult.IsFailure)
                    return Result<Guid>.Fail(vehicleCountByTollPlazaResult.Errors);

                var dataResults = vehicleCountByTollPlazaResult.Data!.Value;

                foreach (var result in dataResults)
                {
                    var vehicleCountByTollPlaza = VehicleCountByTollPlaza.Create(
                        result.TollPlaza,
                        result.VehicleType,
                        result.Count,
                        id
                    );

                    await _unitOfWork.VehicleCountByTollPlazaRepository.InsertAsync(vehicleCountByTollPlaza, cancellationToken);
                }
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("CreateVehicleCountByTollPlazaService finished");

                return Result<Guid>.Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateVehicleCountByTollPlazaService failed");

                return Result<Guid>.Fail(
                    new UnexpectedError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
