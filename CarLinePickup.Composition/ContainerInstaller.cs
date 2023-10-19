using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CarLinePickup.Options;
using CarLinePickup.Composition.Installers;

namespace CarLinePickup.Composition
{
    public static class ContainerInstaller
    {
        public static void AddDependencyGraph(this IServiceCollection services, IOptions<ContainerOptions> options)
        {
            new ContextInstaller(options).Install(services);
            new UnitOfWorkInstaller(options).Install(services);
            new MappingInstaller(options).Install(services);
            new ServiceInstaller(options).Install(services);
        }
    }
}
