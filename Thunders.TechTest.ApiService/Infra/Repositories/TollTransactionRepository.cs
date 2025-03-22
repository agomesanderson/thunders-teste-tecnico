using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Infra.Repositories
{
    public class TollTransactionRepository : Repository<TollTransaction>, ITollTransactionRepository
    {
        public TollTransactionRepository(DbContext context) : base(context) { }
    }
}
