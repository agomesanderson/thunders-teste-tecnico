namespace Thunders.TechTest.ApiService.App.Contracts.Responses
{
    public record GetHourlyRevenueByCityResponse
    {
        public string City { get; init; } = null!;
        public int Hour { get; init; }
        public decimal TotalRevenue { get; init; }
    }
}
