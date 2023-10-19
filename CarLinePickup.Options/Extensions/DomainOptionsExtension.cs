using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarLinePickup.Options.Extensions
{
    public static class DomainOptionsExtension
    {
        public static IServiceCollection AddDomainOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var domainOptions = configuration.GetSection("DomainOptions");

            services.Configure<DomainOptions.DomainOptions>(domainOptions);

            services.PostConfigure<DomainOptions.DomainOptions>(options =>
            {
                //var tileApiKey = configuration.GetValue<string>("TileApiKey");

                //if (string.IsNullOrEmpty(tileApiKey))
                //{
                //    throw new InvalidOperationException("Tile Api Key not initialized");
                //}

                //options.TileOptions.ApiKey = tileApiKey;

            });

            return services;
        }
    }
}
