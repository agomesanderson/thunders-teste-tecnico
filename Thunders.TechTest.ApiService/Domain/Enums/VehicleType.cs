using System.Runtime.Serialization;

namespace Thunders.TechTest.ApiService.Domain.Enums
{
    public enum VehicleType
    {
        [EnumMember(Value = nameof(Car))] Car = 1,
        [EnumMember(Value = nameof(Motorcycle))] Motorcycle = 2,
        [EnumMember(Value = nameof(Truck))] Truck = 3,
    }
}
