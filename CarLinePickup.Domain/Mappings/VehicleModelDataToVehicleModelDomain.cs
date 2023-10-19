using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class VehicleModelDataToVehicleModelDomain : Profile
    {
        public VehicleModelDataToVehicleModelDomain()
        {
            CreateMap<Data.Models.VehicleModel, Models.VehicleModel>()
                .ReverseMap();
        }
    }
}

