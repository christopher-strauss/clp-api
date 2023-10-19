using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class AddressDataToAddressDomain : Profile
    {
        public AddressDataToAddressDomain()
        {
            CreateMap<Data.Models.Address, Models.Address>()
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.State.StateName))
                .ForMember(dest => dest.County, opts => opts.MapFrom(src => src.County.CountyName))
                .ReverseMap()
                .ForMember(dest => dest.County, opts => opts.Ignore())
                .ForMember(dest => dest.State, opts => opts.Ignore());
        }
    }
}

