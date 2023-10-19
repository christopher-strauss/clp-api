using System;
using AutoMapper;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Mappings
{
    public class FamilyMemberDataToFamilyMemberDomain : Profile
    {
        public FamilyMemberDataToFamilyMemberDomain()
        {
            CreateMap<Data.Models.FamilyMember, Models.FamilyMember>()
            .ForMember(dest => dest.FamilyMemberType, opts => opts.MapFrom(src => src.FamilyMemberType.Description))
            .ForMember(dest => dest.FamilyMemberTravelType, opts => opts.MapFrom(src => src.FamilyMemberTravelType.Description))
            .ForMember(dest => dest.FamilyMemberTravelTypeId, opts => opts.MapFrom(src => src.FamilyMemberTravelType.Id))
            .ForMember(dest => dest.FamilyMemberTypeId, opts => opts.MapFrom(src => src.FamilyMemberType.Id))
            .ReverseMap()
            .ForPath(dest => dest.FamilyMemberType.Description, opts => opts.MapFrom(src => src.FamilyMemberType))
            .ForPath(dest => dest.FamilyMemberTravelType.Description, opts => opts.MapFrom(src => src.FamilyMemberTravelType))
            .ForMember(dest => dest.FamilyMemberType, opts => opts.Ignore())
            .ForMember(dest => dest.FamilyMemberTravelType, opts => opts.Ignore())
            .ForMember(dest => dest.EmployeeGrade, opts => opts.Ignore())
            .ForMember(dest => dest.EmployeeGradeId, opts => opts.Ignore())
            .ForMember(dest => dest.FamilyId, opts => opts.Ignore());
        }
    }
}

