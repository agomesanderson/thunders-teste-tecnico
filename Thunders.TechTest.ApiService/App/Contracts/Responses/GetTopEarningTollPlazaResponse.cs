namespace Thunders.TechTest.ApiService.App.Contracts.Responses
{
    public record GetTopEarningTollPlazaResponse
    {
        public string TollPlaza { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
