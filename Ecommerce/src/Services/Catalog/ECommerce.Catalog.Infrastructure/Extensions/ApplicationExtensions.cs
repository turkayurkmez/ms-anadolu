using ECommerce.Catalog.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Extensions
{
    public static class ApplicationExtensions
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<IApplicationMarker>();
                config.RegisterServicesFromAssemblyContaining<IInfrastructureMarker>();
            });

            return services;
        }
    }
}
