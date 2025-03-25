using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Infra.Repositories.Dtos;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repositories.Interfaces
{
    public interface ITollTransactionRepository : IBaseRepository<TollTransaction>
    {
        Task<Result<List<VehicleCoutByTollPlazaDto>>> GetVehicleTypeCountByTollPlazaAsync(
            CancellationToken cancellationToken = default);
        Task<Result<List<HourlyRevenueByCityDto>>> GetTotalAmountByHourAndCityAsync(
            CancellationToken cancellationToken = default);
        Task<Result<List<TopEarningTollPlazaDto>>> GetTopEarningTollPlazasByMonthAsync(
            int limit,
            CancellationToken cancellationToken = default);
    }
}
