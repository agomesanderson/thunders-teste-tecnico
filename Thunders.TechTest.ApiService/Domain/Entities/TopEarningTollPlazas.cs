﻿using System.ComponentModel.DataAnnotations;
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
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalRevenue { get; private init; }

        [Required]
        public Guid ReportId { get; private init; }

        public static TopEarningTollPlazas Create(
            string tollPlaza, 
            decimal totalRevenue, 
            Guid reportId)
        {
            return new()
            {
                TollPlaza = tollPlaza,
                TotalRevenue = totalRevenue,
                ReportId = reportId
            };
        }
    }
}
