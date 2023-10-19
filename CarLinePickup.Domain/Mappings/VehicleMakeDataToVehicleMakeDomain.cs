using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class VehicleMakeDataToVehicleMakeDomain : Profile
    {
        public VehicleMakeDataToVehicleMakeDomain()
        {
            CreateMap<Data.Models.VehicleMake, Models.VehicleMake>()
                .ReverseMap();
        }
    }
}

