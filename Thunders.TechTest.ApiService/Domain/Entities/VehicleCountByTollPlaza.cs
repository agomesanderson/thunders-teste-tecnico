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
    }
}
