using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Infra.Repositories;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Infra.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TollDbContext _context;

        public ITollTransactionRepository TollTransactionRepository { get; }
        public IHourlyRevenueByCityRepository HourlyRevenueByCityRepository { get; }
        public ITopEarningTollPlazasRepository TopEarningTollPlazasRepository { get; }
        public IVehicleCountByTollPlazaRepository VehicleCountByTollPlazaRepository { get; }

        public UnitOfWork(TollDbContext context)
        {
            _context = context;
            TollTransactionRepository = new TollTransactionRepository(context);
            HourlyRevenueByCityRepository = new HourlyRevenueByCityRepository(context);
            TopEarningTollPlazasRepository = new TopEarningTollPlazasRepository(context);
            VehicleCountByTollPlazaRepository = new VehicleCountByTollPlazaRepository(context);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
