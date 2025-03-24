using Thunders.TechTest.ApiService.App.Contracts.Requests;
using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services.Interfaces
{
    public interface ICreateTollTransactionService
    {
        Task<Result<CreateTollTransactionResponse>> Execute(Guid id, CreateTollTransactionRequest request, CancellationToken cancellationToken = default);
    }
}
