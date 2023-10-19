using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarLinePickup.Options.Extensions
{
    public static class AuthorizationOptionsExtension
    {
        public static IServiceCollection AddAuthorizationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var authorizationOptions = configuration.GetSection("AuthorizationOptions");

            services.Configure<AuthorizationOptions>(authorizationOptions);

            return services;
        }
    }
}
