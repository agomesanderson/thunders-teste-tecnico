using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;

namespace Thunders.TechTest.ApiService.Infra.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public IBaseRepository<TollTransaction> TollTransactionRepository { get; }
        //public IRepository<HourlyRevenueByCityReport> HourlyRevenueByCityReportRepository { get; }
        //public IRepository<TopEarningTollPlazasReport> TopEarningTollPlazasReportRepository { get; }
        //public IRepository<VehicleCountByTollPlazaReport> VehicleCountByTollPlazaReportRepository { get; }

        public UnitOfWork(DbContext context)
        {
            _context = context;
            TollTransactionRepository = new BaseRepository<TollTransaction>(context);
            //HourlyRevenueByCityReportRepository = new Repository<HourlyRevenueByCityReport>(context);
            //TopEarningTollPlazasReportRepository = new Repository<TopEarningTollPlazasReport>(context);
            //VehicleCountByTollPlazaReportRepository = new Repository<VehicleCountByTollPlazaReport>(context);
        }

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
