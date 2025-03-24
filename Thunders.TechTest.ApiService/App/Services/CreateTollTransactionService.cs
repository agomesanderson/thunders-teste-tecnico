using Thunders.TechTest.ApiService.App.Contracts.Requests;
using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.App.Services.Errors;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services
{
    public class CreateTollTransactionService : ICreateTollTransactionService
    {
        private readonly ILogger<CreateTollTransactionService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTollTransactionService(
            ILogger<CreateTollTransactionService> logger,
            IUnitOfWork unitOfWork
        ) {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateTollTransactionResponse>> Execute(
            Guid id,
            CreateTollTransactionRequest request,
            CancellationToken cancellationToken = default
        ) {
            try
            {
                _logger.LogInformation("CreateTollTransactionService started");

                var tollTransactionResult = await _unitOfWork.TollTransactionRepository.GetByIdAsync(id, cancellationToken);

                if (tollTransactionResult.IsFailure)
                    return Result<CreateTollTransactionResponse>.Fail(tollTransactionResult.Errors);

                var idempotence = tollTransactionResult.Data.HasValue;

                if (idempotence)
                    return Result<CreateTollTransactionResponse>.Ok(new CreateTollTransactionResponse { Id = id });

                var tollTransaction = TollTransaction.Create(
                    id,
                    request.UsageTime,
                    request.TollPlaza,
                    request.City,
                    request.State,
                    request.AmountPaid,
                    request.VehicleType
                );

                await _unitOfWork.TollTransactionRepository.InsertAsync(tollTransaction, cancellationToken);

                _logger.LogInformation("CreateTollTransactionService finished");

                return Result<CreateTollTransactionResponse>.Ok(new CreateTollTransactionResponse { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateTollTransactionService failed");

                return Result<CreateTollTransactionResponse>.Fail(
                    new UnexpectedError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
