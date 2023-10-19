using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class VehicleDataToVehicleDomain : Profile
    {
        public VehicleDataToVehicleDomain()
        {
            CreateMap<Data.Models.Vehicle, Models.Vehicle>()
                .ForMember(dest => dest.Make, opts => opts.MapFrom(src => src.Make.Name))
                .ForMember(dest => dest.Year, opts => opts.MapFrom(src => src.Model.Year))
                .ForMember(dest => dest.Model, opts => opts.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.ModelId, opts => opts.MapFrom(src => src.Model.Id))
                .ForMember(dest => dest.MakeId, opts => opts.MapFrom(src => src.MakeId))
                .ReverseMap()
                .ForPath(dest => dest.Make.Name, opts => opts.MapFrom(src => src.Make))
                .ForPath(dest => dest.Model.Year, opts => opts.MapFrom(src => src.Year))
                .ForPath(dest => dest.Model.Name, opts => opts.MapFrom(src => src.Model))
                .ForMember(dest => dest.Make, opts => opts.Ignore())
                .ForMember(dest => dest.Model, opts => opts.Ignore())
                .ForMember(dest => dest.FamilyId, opt => opt.Ignore());
        }
    }
}

