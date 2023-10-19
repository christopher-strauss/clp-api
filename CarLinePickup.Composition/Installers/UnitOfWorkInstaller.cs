using CarLinePickup.Composition.Installers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using CarLinePickup.Data.Models;
using CarLinePickup.Options;
using CarLinePickup.Data.Context;
using Arch.EntityFrameworkCore.UnitOfWork;

namespace CarLinePickup.Composition.Installers
{
    public class UnitOfWorkInstaller : IInstaller
    {
        private readonly ContainerOptions _options;

        public UnitOfWorkInstaller(IOptions<ContainerOptions> options)
        {
            _options = options.Value;
        }

        public void Install(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<CarLinePickupContext>>();
        }
    }
}