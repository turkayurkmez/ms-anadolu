using ECommerce.Identity.Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace ECommerce.Identity.API.Extensions
{
    public static class ConfigureServices 
    {
        public static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddSingleton<JwtSettings>(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value); 
            return services;
        }
    }
}
