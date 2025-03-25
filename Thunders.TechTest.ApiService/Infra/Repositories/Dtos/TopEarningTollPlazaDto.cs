namespace Thunders.TechTest.ApiService.Infra.Repositories.Dtos
{
    public record TopEarningTollPlazaDto
    {
        public string TollPlaza { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
