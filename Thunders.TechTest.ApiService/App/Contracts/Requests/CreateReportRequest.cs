using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.App.Contracts.Enums;

namespace Thunders.TechTest.ApiService.App.Contracts.Requests
{
    public record CreateReportRequest
    {
        [Required, EnumDataType(typeof(ReportType))]
        public ReportType Type { get; init; }
    }
}
