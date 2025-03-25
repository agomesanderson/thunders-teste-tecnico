using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repositories.Interfaces
{
    public interface IHourlyRevenueByCityRepository : IBaseRepository<HourlyRevenueByCity>
    {
        Task<Result<HourlyRevenueByCity?>> GetByReportIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<List<HourlyRevenueByCity>>> GetReportAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
