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
        public int Time { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalRevenue { get; private init; }

        [Required]
        public Guid ReportId { get; private init; }

        public static HourlyRevenueByCity Create(
            string city, 
            int hour, 
            decimal totalRevenue, 
            Guid reportId)
        {
            return new()
            {
                City = city,
                Time = hour,
                TotalRevenue = totalRevenue,
                ReportId = reportId
            };
        }
    }
}
