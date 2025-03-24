using Thunders.TechTest.ApiService.Infra.Repositories;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Injections
{
    public static class AddInjections
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITollTransactionRepository, TollTransactionRepository>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITollTransactionRepository, TollTransactionRepository>();

            return services;
        }
    }
}
