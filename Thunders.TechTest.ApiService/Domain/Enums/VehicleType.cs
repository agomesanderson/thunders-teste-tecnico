using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Thunders.TechTest.ApiService.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VehicleType
    {
        [EnumMember(Value = nameof(Car))] Car = 1,
        [EnumMember(Value = nameof(Motorcycle))] Motorcycle = 2,
        [EnumMember(Value = nameof(Truck))] Truck = 3,
    }
}
