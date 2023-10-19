using CarLinePickup.Composition.Installers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CarLinePickup.Data.Models;
using CarLinePickup.Options;
using CarLinePickup.Data.Context;

namespace CarLinePickup.Composition.Installers
{
    public class ContextInstaller : IInstaller
    {
        private readonly ContainerOptions _options;

        public ContextInstaller(IOptions<ContainerOptions> options)
        {
            _options = options.Value;
        }

        public void Install(IServiceCollection services)
        {
            //Add a singleton db context to the DI container
            services.AddDbContext<CarLinePickupContext>(options => options.UseSqlServer(_options.ConnectionString));
        }
    }
}