using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class StateDataToStateDomain : Profile
    {
        public StateDataToStateDomain()
        {
            CreateMap<Data.Models.State, Models.State>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.StateName))
                .ForMember(dest => dest.Abbreviation, opts => opts.MapFrom(src => src.StateAbbreviation))
                .ReverseMap();
        }
    }
}

