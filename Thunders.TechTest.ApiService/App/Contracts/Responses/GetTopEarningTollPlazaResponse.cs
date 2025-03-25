namespace Thunders.TechTest.ApiService.App.Contracts.Responses
{
    public record GetTopEarningTollPlazaResponse
    {
        public string TollPlaza { get; set; } = null!;
        public decimal TotalRevenue { get; set; }
    }
}
