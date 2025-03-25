using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thunders.TechTest.ApiService.Domain.Entities.Base;
using Thunders.TechTest.ApiService.Domain.Enums;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    [Table("VehicleCountByTollPlaza")]
    public class VehicleCountByTollPlaza : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string TollPlaza { get; private init; } = null!;

        [Required]
        public VehicleType VehicleType { get; private init; }

        [Required]
        public int VehicleCount { get; private init; }

        [Required]
        public Guid ReportId { get; private init; }

        public static VehicleCountByTollPlaza Create(
            string tollPlaza, 
            VehicleType vehicleType, 
            int vehicleCount, 
            Guid reportId)
        {
            return new()
            {
                TollPlaza = tollPlaza,
                VehicleType = vehicleType,
                VehicleCount = vehicleCount,
                ReportId = reportId
            };
        }
    }
}
