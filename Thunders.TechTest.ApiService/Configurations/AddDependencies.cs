using Thunders.TechTest.ApiService.App.Services;
using Thunders.TechTest.ApiService.App.Services.Interfaces;
using Thunders.TechTest.ApiService.Infra.Repositories;
using Thunders.TechTest.ApiService.Infra.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Configurations
{
    public static class AddDependencies
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateTollTransactionService, CreateTollTransactionService>();
            services.AddScoped<ICreateReportService, CreateReportService>();
            services.AddScoped<ICreateVehicleCountByTollPlazaService, CreateVehicleCountByTollPlazaService>();
            services.AddScoped<ICreateTopEarningTollPlazaService, CreateTopEarningTollPlazaService>();
            services.AddScoped<ICreateHourlyRevenueService, CreateHourlyRevenueService>();

            services.AddScoped<IGetVehicleCountByTollPlazaService, GetVehicleCountByTollPlazaService>();
            services.AddScoped<IGetTopEarningTollPlazaService, GetTopEarningTollPlazaService>();
            services.AddScoped<IGetHourlyRevenueService, GetHourlyRevenueService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITollTransactionRepository, TollTransactionRepository>();
            services.AddScoped<IHourlyRevenueByCityRepository, HourlyRevenueByCityRepository>();
            services.AddScoped<ITopEarningTollPlazasRepository, TopEarningTollPlazasRepository>();
            services.AddScoped<IVehicleCountByTollPlazaRepository, VehicleCountByTollPlazaRepository>();

            return services;
        }
    }
}
