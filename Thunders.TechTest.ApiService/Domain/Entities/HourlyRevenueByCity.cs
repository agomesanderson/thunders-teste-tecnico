using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.Domain.Entities.Base;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    [Table("HourlyRevenueByCity")]
    public class HourlyRevenueByCity : BaseReportEintity
    {
        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [Required]
        public DateTime Hour { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalRevenue { get; set; }
    }
}
