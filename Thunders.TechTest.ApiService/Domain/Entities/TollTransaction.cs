using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thunders.TechTest.ApiService.Domain.Entities.Base;
using Thunders.TechTest.ApiService.Domain.Enums;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    [Table("ToolTransactions")]
    public class TollTransaction : BaseEntity
    {
        [Required]
        public DateTime UsageTime { get; private init; }

        [Required]
        [StringLength(100)]
        public string TollPlaza { get; private init; } = null!;

        [Required]
        [StringLength(100)]
        public string City { get; private init; } = null!;

        [Required]
        [StringLength(50)]
        public FederationUnit State { get; private init; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AmountPaid { get; private init; }

        [Required]
        public VehicleType VehicleType { get; private init; }

        public static TollTransaction Create(
            Guid id,
            DateTime usageTime, 
            string tollPlaza, 
            string city, 
            FederationUnit state, 
            decimal amountPaid, 
            VehicleType vehicleType)
        {
            return new()
            {
                Id = id,
                UsageTime = usageTime,
                TollPlaza = tollPlaza,
                City = city,
                State = state,
                AmountPaid = amountPaid,
                VehicleType = vehicleType
            };
        }
    }
}
