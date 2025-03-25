using Thunders.TechTest.ApiService.Domain.Enums;

namespace Thunders.TechTest.ApiService.App.Contracts.Responses
{
    public record GetVehicleCountByTollPlazaResponse
    {
        public string TollPlaza { get; init; } = null!;
        public VehicleType VehicleType { get; init; }
        public int Count { get; init; }
    }
}
