using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class SchoolDataToSchoolDomain : Profile
    {
        public SchoolDataToSchoolDomain()
        {
            CreateMap<Data.Models.School, Models.School>()
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.SchoolName))
            .ReverseMap();
        }
    }
}

