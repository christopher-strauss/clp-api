using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class FamilyMemberTravelTypeDataToFamilyMemberTravelTypeDomain : Profile
    {
        public FamilyMemberTravelTypeDataToFamilyMemberTravelTypeDomain()
        {
            CreateMap<Data.Models.FamilyMemberTravelType, Models.FamilyMemberTravelType>()
                .ReverseMap();
        }
    }
}

