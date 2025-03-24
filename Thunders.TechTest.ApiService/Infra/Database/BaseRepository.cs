using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Entities.Base;
using Thunders.TechTest.ApiService.Infra.Database.Errors;
using Thunders.TechTest.ApiService.Infra.Database.Interfaces;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Infra.Database
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<Result<T?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id, cancellationToken);
                return Result<T?>.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result<T?>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message));
            }
        }

        public async Task<Result<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var entities = await _dbSet.ToListAsync(cancellationToken);
                return Result<IEnumerable<T>>.Ok(entities);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<T>>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }

        public async Task<Result<T>> InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbSet.AddAsync(entity, cancellationToken);
                return Result<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }

        public Result<T> Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                return Result<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }

        public Result<T> Remove(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return Result<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(
                    new DatabaseError(ex.InnerException?.Message ?? ex.Message)
                );
            }
        }
    }
}
