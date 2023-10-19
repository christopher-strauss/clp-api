using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class VehicleMakeDomainToVehicleMakeResponse : Profile
    {
        public VehicleMakeDomainToVehicleMakeResponse()
        {
            CreateMap<Domain.Models.VehicleMake, VehicleMakeResponse>();
        }
    }
}
