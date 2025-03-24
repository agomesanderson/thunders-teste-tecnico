using Thunders.TechTest.ApiService.Domain.Entities;

namespace Thunders.TechTest.ApiService.Infra.Database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        BaseRepository<TollTransaction> TollTransactionRepository { get; }
        //IRepository<HourlyRevenueByCityReport> HourlyRevenueByCityReportRepository { get; }
        //IRepository<TopEarningTollPlazasReport> TopEarningTollPlazasReportRepository { get; }
        //IRepository<VehicleCountByTollPlazaReport> VehicleCountByTollPlazaReportRepository { get; }
        Task<int> CommitAsync();
    }
}
