using Thunders.TechTest.ApiService.App.Services.Errors;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services
{
    public class CreateTopEarningTollPlazaService : ICreateTopEarningTollPlazaService
    {
        private readonly ILogger<CreateTopEarningTollPlazaService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public CreateTopEarningTollPlazaService(
            ILogger<CreateTopEarningTollPlazaService> logger,
            IUnitOfWork unitOfWork,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<Result<Guid>> Execute(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                _logger.LogInformation("CreateTopEarningTollPlazaService started");

                var topEarningReportResult = await _unitOfWork.TopEarningTollPlazasRepository.GetByReportIdAsync(id, cancellationToken);

                if (topEarningReportResult.IsFailure)
                    return Result<Guid>.Fail(topEarningReportResult.Errors);

                var idempotence = topEarningReportResult.Data.HasValue;

                if (idempotence)
                    return Result<Guid>.Ok(id);

                var limitMaxTollPlazas = _configuration.GetValue<int>("Reports:LimitMaxTollPlazas");
                var topEarningResult = await _unitOfWork.TollTransactionRepository.GetTopEarningTollPlazasByMonthAsync(limitMaxTollPlazas, cancellationToken);

                if (topEarningResult.IsFailure)
                    return Result<Guid>.Fail(topEarningResult.Errors);

                var dataResults = topEarningResult.Data!.Value;

                foreach (var result in dataResults)
                {
                    var topEarning = TopEarningTollPlazas.Create(
                        result.TollPlaza,
                        result.TotalAmount,
                        result.Month,
                        result.Year,
                        id
                    );

                    await _unitOfWork.TopEarningTollPlazasRepository.InsertAsync(topEarning, cancellationToken);
                }
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("CreateTopEarningTollPlazaService finished");

                return Result<Guid>.Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateTopEarningTollPlazaService failed");

                return Result<Guid>.Fail(
                    new UnexpectedError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
