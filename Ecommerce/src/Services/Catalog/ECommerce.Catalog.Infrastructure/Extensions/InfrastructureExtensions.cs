using ECommerce.Catalog.Application.Contracts;
using ECommerce.Catalog.Infrastructure.Persistence;
using ECommerce.Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration.GetSection("DefaultHostName").Value;
            var pass = configuration.GetSection("DefaultPassword").Value;




            var connectionString = configuration.GetConnectionString("DefaultConnection");
            connectionString = connectionString.Replace("[PASS]", pass).Replace("[HOST]", host);
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(connectionString, b=>b.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName)) );
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }

    public static class  DatabaseInitializer
    {
        public static async Task CreateAndSeedDatabaseAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var catalogDbContext = services.GetRequiredService<CatalogDbContext>();
            await catalogDbContext.Database.MigrateAsync();
        }
     }
}
