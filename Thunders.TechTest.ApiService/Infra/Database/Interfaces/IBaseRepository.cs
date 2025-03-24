using Thunders.TechTest.ApiService.Domain.Entities.Base;
using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.Infra.Database.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<Result<T?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<T>> InsertAsync(T entity, CancellationToken cancellationToken = default);
        Result<T> Update(T entity);
        Result<T> Remove(T entity);
    }
}
