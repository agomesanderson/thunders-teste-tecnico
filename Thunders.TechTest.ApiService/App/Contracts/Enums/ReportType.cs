using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Thunders.TechTest.ApiService.App.Contracts.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReportType
    {
        [EnumMember(Value = nameof(HourlyRevenueByCity))] HourlyRevenueByCity = 1,
        [EnumMember(Value = nameof(TopEarningTollPlaza))] TopEarningTollPlaza = 2,
        [EnumMember(Value = nameof(VehicleCountByTollPlaza))] VehicleCountByTollPlaza = 3,
    }
}
