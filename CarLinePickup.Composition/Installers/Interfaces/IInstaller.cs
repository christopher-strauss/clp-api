using Microsoft.Extensions.DependencyInjection;

namespace CarLinePickup.Composition.Installers.Interfaces
{
    interface IInstaller
    {
        void Install(IServiceCollection services);
    }
}
