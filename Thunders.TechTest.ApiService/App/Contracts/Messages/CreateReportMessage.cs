using Thunders.TechTest.ApiService.App.Contracts.Enums;

namespace Thunders.TechTest.ApiService.App.Contracts.Messages
{
    public record CreateReportMessage
    {
        public Guid Id { get; set; }
        public ReportType Type { get; set; }
    }
}
