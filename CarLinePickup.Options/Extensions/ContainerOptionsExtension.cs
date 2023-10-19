using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarLinePickup.Options.Extensions
{
    public static class ContainerOptionsExtension
    {
        public static IServiceCollection AddContainerOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var containerOptions = configuration.GetSection("ContainerOptions");

            services.Configure<ContainerOptions>(containerOptions);

            services.PostConfigure<ContainerOptions>(options =>
            {
                var connectionString = configuration.GetValue<string>("ConnectionStrings:CarLinePickupDev");
                //var tileAPIKey = configuration.GetValue<string>("TileAPIKey");

                //if (string.IsNullOrEmpty(tileAPIKey))
                //    throw new InvalidOperationException("Tile Api Key not initialized");

                if (String.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string not initialized");

                options.ConnectionString = connectionString;
            });

            return services;
        }
    }
}

