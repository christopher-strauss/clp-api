using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class GradeDataToGradeDomain : Profile
    {
        public GradeDataToGradeDomain()
        {
            CreateMap<Data.Models.Grade, Models.Grade>()
            .ReverseMap();
        }
    }
}

