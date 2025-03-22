using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thunders.TechTest.ApiService.Domain.Entities.Base;
using Thunders.TechTest.ApiService.Domain.Enums;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    [Table("VehicleCountByTollPlaza")]
    public class VehicleCountByTollPlaza : BaseReportEintity
    {
        [Required]
        [StringLength(100)]
        public string TollPlaza { get; set; } = null!;

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        public int VehicleCount { get; set; }
    }
}
