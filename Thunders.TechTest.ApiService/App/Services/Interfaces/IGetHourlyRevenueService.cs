using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services.Interfaces
{
    public interface IGetHourlyRevenueService
    {
        Task<Result<List<GetHourlyRevenueByCityResponse>>> Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
