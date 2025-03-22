using System.ComponentModel.DataAnnotations;

namespace Thunders.TechTest.ApiService.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
    }
}
