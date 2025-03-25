using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Infra.Repositories
{
    public class HourlyRevenueByCityRepository : BaseRepository<HourlyRevenueByCity>, IHourlyRevenueByCityRepository
    {
        public HourlyRevenueByCityRepository(TollDbContext context) : base(context) { }
    }
}
