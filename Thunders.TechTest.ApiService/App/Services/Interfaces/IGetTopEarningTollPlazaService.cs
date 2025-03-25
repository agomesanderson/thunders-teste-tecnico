using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services.Interfaces
{
    public interface IGetTopEarningTollPlazaService
    {
        Task<Result<List<GetTopEarningTollPlazaResponse>>> Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
