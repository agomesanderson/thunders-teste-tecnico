using Thunders.TechTest.ApiService.Shared;

namespace Thunders.TechTest.ApiService.App.Services.Interfaces
{
    public interface ICreateVehicleCountByTollPlazaService
    {
        Task<Result<Guid>> Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
