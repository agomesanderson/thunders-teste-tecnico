using Thunders.TechTest.ApiService.Infra.Repositories;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Infra.Database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITollTransactionRepository TollTransactionRepository { get; }
        IHourlyRevenueByCityRepository HourlyRevenueByCityRepository { get; }
        ITopEarningTollPlazasRepository TopEarningTollPlazasRepository { get; }
        IVehicleCountByTollPlazaRepository VehicleCountByTollPlazaRepository { get; }
        Task<int> SaveAsync();
    }
}
