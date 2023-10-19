using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using AutoMapper;
using CarLinePickup.Options;
using CarLinePickup.Composition.Installers.Interfaces;

namespace CarLinePickup.Composition.Installers
{
    public class MappingInstaller : IInstaller
    {
        private readonly ContainerOptions _options;
        private readonly string[] mapAssemblies = new[] { "CarLinePickup.API", "CarLinePickup.Domain" };

        public MappingInstaller(IOptions<ContainerOptions> options)
        {
            _options = options.Value;
        }

        public void Install(IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
                                    cfg.AddMaps(mapAssemblies));

            services.AddSingleton<IMapper>(s => configuration.CreateMapper());
        }
    }
}