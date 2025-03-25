using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Infra.Repositories
{
    public class TopEarningTollPlazasRepository : BaseRepository<TopEarningTollPlazas>, ITopEarningTollPlazasRepository
    {
        public TopEarningTollPlazasRepository(TollDbContext context) : base(context) { }
    }
}
