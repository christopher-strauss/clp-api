using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class VehicleDomainToVehicleResponse : Profile
    {
        public VehicleDomainToVehicleResponse()
        {
            CreateMap<Domain.Models.Vehicle, VehicleResponse>();
        }
    }
}
