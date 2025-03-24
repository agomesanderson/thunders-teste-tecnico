using Microsoft.EntityFrameworkCore;

namespace Thunders.TechTest.ApiService.Infra.Database.Extensions
{
    public static class DatabaseExtensions
    {
        public static WebApplication MigrateDatabase<TContext>(this WebApplication host) where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();

            scope.ServiceProvider.GetService<TContext>()!.Database.Migrate();
            return host;
        }
    }
}
