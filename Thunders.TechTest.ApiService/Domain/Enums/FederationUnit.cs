using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Thunders.TechTest.ApiService.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FederationUnit
    {
        [EnumMember(Value = nameof(AM))] AM,
        [EnumMember(Value = nameof(AL))] AL,
        [EnumMember(Value = nameof(AC))] AC,
        [EnumMember(Value = nameof(AP))] AP,
        [EnumMember(Value = nameof(BA))] BA,
        [EnumMember(Value = nameof(MT))] MT,
        [EnumMember(Value = nameof(MG))] MG,
        [EnumMember(Value = nameof(MS))] MS,
        [EnumMember(Value = nameof(GO))] GO,
        [EnumMember(Value = nameof(MA))] MA,
        [EnumMember(Value = nameof(RS))] RS,
        [EnumMember(Value = nameof(TO))] TO,
        [EnumMember(Value = nameof(PI))] PI,
        [EnumMember(Value = nameof(SP))] SP,
        [EnumMember(Value = nameof(RO))] RO,
        [EnumMember(Value = nameof(RR))] RR,
        [EnumMember(Value = nameof(PR))] PR,
        [EnumMember(Value = nameof(CE))] CE,
        [EnumMember(Value = nameof(PE))] PE,
        [EnumMember(Value = nameof(SC))] SC,
        [EnumMember(Value = nameof(PB))] PB,
        [EnumMember(Value = nameof(RN))] RN,
        [EnumMember(Value = nameof(ES))] ES,
        [EnumMember(Value = nameof(RJ))] RJ,
        [EnumMember(Value = nameof(SE))] SE,
        [EnumMember(Value = nameof(DF))] DF,
    }
}
