using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Database;
using Thunders.TechTest.ApiService.Infra.Database.Errors;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repositories
{
    public class VehicleCountByTollPlazaRepository : BaseRepository<VehicleCountByTollPlaza>, IVehicleCountByTollPlazaRepository
    {
        public VehicleCountByTollPlazaRepository(TollDbContext context) : base(context) { 
        
        }

        public async Task<Result<VehicleCountByTollPlaza?>> GetByReportIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _dbSet.FirstOrDefaultAsync(x => x.ReportId == id, cancellationToken);
                return Result<VehicleCountByTollPlaza?>.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result<VehicleCountByTollPlaza?>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message));
            }
        }

        public async Task<Result<List<VehicleCountByTollPlaza>>> GetReportAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entities = await _dbSet
                    .Where(x => x.ReportId == id)
                    .ToListAsync(cancellationToken);

                return Result<List<VehicleCountByTollPlaza>>.Ok(entities);
            }
            catch (Exception ex)
            {
                return Result<List<VehicleCountByTollPlaza>>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
