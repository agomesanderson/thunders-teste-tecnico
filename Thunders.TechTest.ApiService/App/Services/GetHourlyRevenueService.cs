using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.App.Services.Errors;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services
{
    public class GetHourlyRevenueService : IGetHourlyRevenueService
    {
        private readonly ILogger<GetHourlyRevenueService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetHourlyRevenueService(
            ILogger<GetHourlyRevenueService> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<GetHourlyRevenueByCityResponse>>> Execute(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                _logger.LogInformation("GetHourlyRevenueService started");

                var topEarningTollPlazaReportResult = await _unitOfWork.HourlyRevenueByCityRepository.GetReportAsync(id, cancellationToken);

                if (topEarningTollPlazaReportResult.IsFailure)
                    return Result<List<GetHourlyRevenueByCityResponse>>.Fail(topEarningTollPlazaReportResult.Errors);

                var hasData = topEarningTollPlazaReportResult.Data.Match(
                  some:
                    value => value.Count > 0,
                  none:
                    () => false
                );

                if (!hasData)
                    return Result<List<GetHourlyRevenueByCityResponse>>.Fail(new NotFoundError("Report not found"));

                var dataResult = topEarningTollPlazaReportResult.Data!.Value;

                var report = dataResult.Select(x => 
                    new GetHourlyRevenueByCityResponse
                    { 
                        City = x.City, 
                        Hour = x.Time,
                        TotalRevenue = x.TotalRevenue
                    }).ToList();

                return Result<List<GetHourlyRevenueByCityResponse>>.Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetHourlyRevenueService failed");

                return Result<List<GetHourlyRevenueByCityResponse>>.Fail(
                    new UnexpectedError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
