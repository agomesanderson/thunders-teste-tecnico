using Thunders.TechTest.ApiService.App.Contracts.Enums;

namespace Thunders.TechTest.ApiService.App.Contracts.Responses
{
    public record CreateReportResponse
    {
        public Guid Id { get; init; }
        public ReportType Type { get; init; }
    }
}
