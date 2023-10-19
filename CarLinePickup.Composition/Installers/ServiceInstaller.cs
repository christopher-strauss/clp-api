using CarLinePickup.Composition.Installers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CarLinePickup.Domain.Models;
using CarLinePickup.Domain.Services;
using CarLinePickup.Domain.Services.Interfaces;
using CarLinePickup.Options;
using Flurl.Http.Configuration;

namespace CarLinePickup.Composition.Installers
{
    public class ServiceInstaller : IInstaller
    {
        private readonly ContainerOptions _options;

        public ServiceInstaller(IOptions<ContainerOptions> options)
        {
            _options = options.Value;
        }

        public void Install(IServiceCollection services)
        {
            services.AddScoped<IAddressService<Address>, AddressService>();
            services.AddScoped<IEmployeeService<Employee>, EmployeeService>();
            services.AddScoped<IEmployeeTypeService<EmployeeType>, EmployeeTypeService>();
            services.AddScoped<IFamilyMemberService<FamilyMember>, FamilyMemberService>();
            services.AddScoped<ISchoolService<School>, SchoolService>();
            services.AddScoped<IVehicleService<Vehicle>, VehicleService>();
            services.AddScoped<IVehicleMakeService<VehicleMake>, VehicleMakeService>();
            services.AddScoped<IVehicleModelService<VehicleModel>, VehicleModelService>();
            services.AddScoped<IFamilyService<Family>, FamilyService>();
            services.AddScoped<IGradeService<Grade>, GradeService>();
            services.AddScoped<ICountyService<County>, CountyService>();
            services.AddScoped<IStateService<State>, StateService>();
            services.AddScoped<IAuthenticationUserService<AuthenticationUser>, AuthenticationUserService>();
            services.AddScoped<IAuthenticationUserProviderService<AuthenticationUserProvider>, AuthenticationUserProviderService>();
            services.AddScoped<IAuthenticationUserTypeService<AuthenticationUserType>, AuthenticationUserTypeService>();
            services.AddScoped<IFamilyMemberTypeService<FamilyMemberType>, FamilyMemberTypeService>();
            services.AddScoped<IFamilyMemberTravelTypeService<FamilyMemberTravelType>, FamilyMemberTravelTypeService>();
            services.AddScoped<IVPICService, VPICService>();
            services.AddSingleton<IFlurlClientFactory, PerHostFlurlClientFactory>();
        }
    }
}