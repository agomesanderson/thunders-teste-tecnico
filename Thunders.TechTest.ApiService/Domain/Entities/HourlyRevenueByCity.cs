using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.Domain.Entities.Base;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    [Table("HourlyRevenueByCity")]
    public class HourlyRevenueByCity : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string City { get; private init; } = null!;

        [Required]
        public DateTime Hour { get; private init; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalRevenue { get; private init; }
    }
}
