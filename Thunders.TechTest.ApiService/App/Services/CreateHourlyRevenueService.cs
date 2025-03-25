using Thunders.TechTest.ApiService.App.Services.Errors;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services
{
    public class CreateHourlyRevenueService : ICreateHourlyRevenueService
    {
        private readonly ILogger<CreateHourlyRevenueService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateHourlyRevenueService(
            ILogger<CreateHourlyRevenueService> logger,
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
                _logger.LogInformation("CreateHourlyRevenueService started");

                var hourlyRevenueReportResult = await _unitOfWork.HourlyRevenueByCityRepository.GetByReportIdAsync(id, cancellationToken);

                if (hourlyRevenueReportResult.IsFailure)
                    return Result<Guid>.Fail(hourlyRevenueReportResult.Errors);

                var idempotence = hourlyRevenueReportResult.Data.HasValue;

                if (idempotence)
                    return Result<Guid>.Ok(id);

                var hourlyRevenueResult = await _unitOfWork.TollTransactionRepository.GetTotalAmountByHourAndCityAsync(cancellationToken);

                if (hourlyRevenueResult.IsFailure)
                    return Result<Guid>.Fail(hourlyRevenueResult.Errors);

                var dataResults = hourlyRevenueResult.Data!.Value;

                foreach (var result in dataResults)
                {
                    var hourlyRevenue = HourlyRevenueByCity.Create(
                        result.City,
                        result.Hour,
                        result.TotalAmount,
                        id
                    );

                    await _unitOfWork.HourlyRevenueByCityRepository.InsertAsync(hourlyRevenue, cancellationToken);
                }
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("CreateHourlyRevenueService finished");

                return Result<Guid>.Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateHourlyRevenueService failed");

                return Result<Guid>.Fail(
                    new UnexpectedError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
