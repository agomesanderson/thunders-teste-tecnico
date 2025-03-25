using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repositories.Interfaces
{
    public interface ITopEarningTollPlazasRepository : IBaseRepository<TopEarningTollPlazas>
    {
        Task<Result<TopEarningTollPlazas?>> GetByReportIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<List<TopEarningTollPlazas>>> GetReportAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
