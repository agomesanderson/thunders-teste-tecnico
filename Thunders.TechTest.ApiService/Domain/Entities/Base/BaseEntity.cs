using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
    }
}
