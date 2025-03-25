using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.App.Services.Errors;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services
{
    public class GetTopEarningTollPlazaService : IGetTopEarningTollPlazaService
    {
        private readonly ILogger<GetTopEarningTollPlazaService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetTopEarningTollPlazaService(
            ILogger<GetTopEarningTollPlazaService> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<GetTopEarningTollPlazaResponse>>> Execute(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                _logger.LogInformation("GetTopEarningTollPlazaService started");

                var topEarningTollPlazaReportResult = await _unitOfWork.TopEarningTollPlazasRepository.GetReportAsync(id, cancellationToken);

                if (topEarningTollPlazaReportResult.IsFailure)
                    return Result<List<GetTopEarningTollPlazaResponse>>.Fail(topEarningTollPlazaReportResult.Errors);

                var hasData = topEarningTollPlazaReportResult.Data.Match(
                  some:
                    value => value.Count > 0,
                  none:
                    () => false
                );

                if (!hasData)
                    return Result<List<GetTopEarningTollPlazaResponse>>.Fail(new NotFoundError("Report not found"));

                var dataResult = topEarningTollPlazaReportResult.Data!.Value;

                var report = dataResult.Select(x => 
                    new GetTopEarningTollPlazaResponse { 
                        TollPlaza = x.TollPlaza, 
                        TotalRevenue = x.TotalRevenue
                    }).ToList();

                return Result<List<GetTopEarningTollPlazaResponse>>.Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetTopEarningTollPlazaService failed");

                return Result<List<GetTopEarningTollPlazaResponse>>.Fail(
                    new UnexpectedError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
