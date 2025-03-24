using Microsoft.AspNetCore.Mvc;

namespace Thunders.TechTest.ApiService.Configurations
{
    public static class ApiVersioning
    {
        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
    }
}
