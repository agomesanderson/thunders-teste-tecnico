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
        public DateTime UsageTime { get; set; }

        [Required]
        [StringLength(100)]
        public string TollPlaza { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public FederationUnit State { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AmountPaid { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }
    }
}
