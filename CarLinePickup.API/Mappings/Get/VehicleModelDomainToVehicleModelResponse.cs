using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class VehicleModelDomainToVehicleModelResponse : Profile
    {
        public VehicleModelDomainToVehicleModelResponse()
        {
            CreateMap<Domain.Models.VehicleModel, VehicleModelResponse>();
        }
    }
}
