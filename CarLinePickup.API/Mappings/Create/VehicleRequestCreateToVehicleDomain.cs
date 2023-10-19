using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class VehicleRequestCreateToVehicleDomain : Profile
    {
        public VehicleRequestCreateToVehicleDomain()
        {
            CreateMap<VehicleRequestCreate, Domain.Models.Vehicle>()
                .ReverseMap();
        }
    }
}
