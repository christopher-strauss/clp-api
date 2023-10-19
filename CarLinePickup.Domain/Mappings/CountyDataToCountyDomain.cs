using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class CountyDataToCountyDomain : Profile
    {
        public CountyDataToCountyDomain()
        {
            CreateMap<Data.Models.County, Models.County>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.CountyName))
                .ReverseMap()
                .ForMember(dest => dest.StateId, opt => opt.Ignore());
        }
    }
}

