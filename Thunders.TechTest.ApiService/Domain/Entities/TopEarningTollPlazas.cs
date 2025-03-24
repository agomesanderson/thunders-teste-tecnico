using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thunders.TechTest.ApiService.Domain.Entities.Base;

namespace Thunders.TechTest.ApiService.Domain.Entities
{
    [Table("TopEarningTollPlazas")]
    public class TopEarningTollPlazas : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string TollPlaza { get; private init; } = null!;

        [Required]
        public decimal TotalRevenue { get; private init; }
    }
}
