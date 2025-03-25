namespace Thunders.TechTest.ApiService.Infra.Repositories.Dtos
{
    public record HourlyRevenueByCityDto
    {
        public string City { get; set; } = null!;
        public int Hour { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
