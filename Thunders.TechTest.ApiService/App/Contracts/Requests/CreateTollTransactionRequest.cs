using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.Domain.Enums;
using Thunders.TechTest.ApiService.Shared.Constants;

namespace Thunders.TechTest.ApiService.App.Contracts.Requests
{
    public record CreateTollTransactionRequest
    {
        [Required]
        public DateTime UsageTime { get; init; }

        [Required, MaxLength(100)]
        public string TollPlaza { get; init; } = null!;

        [Required, MaxLength(100)]
        public string City { get; init; } = null!;

        [Required, EnumDataType(typeof(FederationUnit))]
        public FederationUnit State { get; init; }

        [Required]
        public decimal AmountPaid { get; init; }

        [Required, EnumDataType(typeof(VehicleType))]
        public VehicleType VehicleType { get; init; }
    }
}
