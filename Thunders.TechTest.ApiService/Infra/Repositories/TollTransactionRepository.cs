using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Infra.Repositories
{
    public class TollTransactionRepository : BaseRepository<TollTransaction>, ITollTransactionRepository
    {
        public TollTransactionRepository(TollDbContext context) : base(context) { }
    }
}
