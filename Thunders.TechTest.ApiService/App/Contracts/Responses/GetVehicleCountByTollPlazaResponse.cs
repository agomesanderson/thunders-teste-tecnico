using Thunders.TechTest.ApiService.Domain.Enums;

namespace Thunders.TechTest.ApiService.App.Contracts.Responses
{
    public record GetVehicleCountByTollPlazaResponse
    {
        public string TollPlaza { get; set; } = null!;
        public VehicleType VehicleType { get; set; }
        public int Count { get; set; }
    }
}
