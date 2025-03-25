using Thunders.TechTest.ApiService.App.Contracts.Requests;
using Thunders.TechTest.ApiService.App.Contracts.Responses;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services.Interfaces
{
    public interface ICreateReportService
    {
        Task<Result<CreateReportResponse>> Execute(Guid id, CreateReportRequest request, CancellationToken cancellationToken = default);
    }
}
