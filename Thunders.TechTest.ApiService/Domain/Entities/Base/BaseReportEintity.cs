using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Domain.Entities.Base
{
    public class BaseReportEintity : BaseEntity
    {
        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
