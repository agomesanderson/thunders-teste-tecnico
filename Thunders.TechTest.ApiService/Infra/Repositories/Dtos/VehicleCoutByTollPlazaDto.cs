using Thunders.TechTest.ApiService.Domain.Enums;

namespace Thunders.TechTest.ApiService.Infra.Repositories.Dtos
{
    public record VehicleCoutByTollPlazaDto
    {
        public string TollPlaza { get; set; } = null!;
        public VehicleType VehicleType { get; set; }
        public int Count { get; set; }
    }
}
