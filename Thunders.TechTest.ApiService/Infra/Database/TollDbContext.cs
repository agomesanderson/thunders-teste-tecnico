using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Entities;

namespace Thunders.TechTest.ApiService.Infra.Database
{
    public class TollDbContext : DbContext
    {
        public TollDbContext(DbContextOptions<TollDbContext> options) : base(options) { }

        public DbSet<TollTransaction> TollTransactions { get; set; }
        public DbSet<HourlyRevenueByCity> HourlyRevenueByCitys { get; set; }
        public DbSet<TopEarningTollPlazas> TopEarningTollPlazas { get; set; }
        public DbSet<VehicleCountByTollPlaza> VehicleCountByTollPlazas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TollTransaction>().ToTable("TollTransaction");
            modelBuilder.Entity<HourlyRevenueByCity>().ToTable("HourlyRevenueByCity");
            modelBuilder.Entity<TopEarningTollPlazas>().ToTable("TopEarningTollPlazas");
            modelBuilder.Entity<VehicleCountByTollPlaza>().ToTable("VehicleCountByTollPlaza");
        }
    }
}
