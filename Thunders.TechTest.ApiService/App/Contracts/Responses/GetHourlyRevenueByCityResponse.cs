namespace Thunders.TechTest.ApiService.App.Contracts.Responses
{
    public record GetHourlyRevenueByCityResponse
    {
        public string City { get; set; } = null!;
        public int Hour { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
