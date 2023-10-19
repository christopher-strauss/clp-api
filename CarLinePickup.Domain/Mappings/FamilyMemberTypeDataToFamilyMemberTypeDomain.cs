using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class FamilyMemberTypeDataToFamilyMemberTypeDomain : Profile
    {
        public FamilyMemberTypeDataToFamilyMemberTypeDomain()
        {
            CreateMap<Data.Models.FamilyMemberType, Models.FamilyMemberType>()
                .ReverseMap();
        }
    }
}