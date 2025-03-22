using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thunders.TechTest.ApiService.Domain.Entities.Base;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    [Table("TopEarningTollPlazas")]
    public class TopEarningTollPlazas : BaseReportEintity
    {
        [Required]
        [StringLength(100)]
        public string TollPlaza { get; set; } = null!;

        [Required]
        public decimal TotalRevenue { get; set; }
    }
}
