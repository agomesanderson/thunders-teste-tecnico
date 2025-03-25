using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database;
using Thunders.TechTest.ApiService.Infra.Database.Errors;
using Thunders.TechTest.ApiService.Infra.Repositories.Dtos;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repositories
{
    public class TollTransactionRepository : BaseRepository<TollTransaction>, ITollTransactionRepository
    {
        public TollTransactionRepository(TollDbContext context) : base(context) { }

        public async Task<Result<List<VehicleCoutByTollPlazaDto>>> GetVehicleTypeCountByTollPlazaAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _dbSet
                    .GroupBy(t => new { t.TollPlaza, t.VehicleType })
                    .Select(g => new VehicleCoutByTollPlazaDto
                    {
                        TollPlaza = g.Key.TollPlaza,
                        VehicleType = g.Key.VehicleType,
                        Count = g.Count()
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<VehicleCoutByTollPlazaDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Result<List<VehicleCoutByTollPlazaDto>>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message));
            }
        }

        public async Task<Result<List<HourlyRevenueByCityDto>>> GetTotalAmountByHourAndCityAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _dbSet
                    .GroupBy(t => new { t.City, Hour = t.UsageTime.Hour })
                    .Select(g => new HourlyRevenueByCityDto
                    {
                        City = g.Key.City,
                        Hour = g.Key.Hour,
                        TotalAmount = g.Sum(t => t.AmountPaid)
                    })
                    .ToListAsync(cancellationToken);

                return Result<List<HourlyRevenueByCityDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Result<List<HourlyRevenueByCityDto>>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message));
            }
        }

        public async Task<Result<List<TopEarningTollPlazaDto>>> GetTopEarningTollPlazasByMonthAsync(
            int limit, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _dbSet
                    .GroupBy(t => new { t.TollPlaza, Month = t.UsageTime.Month, Year = t.UsageTime.Year })
                    .Select(g => new TopEarningTollPlazaDto
                    {
                        TollPlaza = g.Key.TollPlaza,
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        TotalAmount = g.Sum(t => t.AmountPaid)
                    })
                    .OrderByDescending(t => t.TotalAmount)
                    .Take(limit)
                    .ToListAsync(cancellationToken);

                return Result<List<TopEarningTollPlazaDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Result<List<TopEarningTollPlazaDto>>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
