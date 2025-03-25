using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repositories.Interfaces
{
    public interface IVehicleCountByTollPlazaRepository : IBaseRepository<VehicleCountByTollPlaza>
    {
        Task<Result<VehicleCountByTollPlaza?>> GetByReportIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<List<VehicleCountByTollPlaza>>> GetReportAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
