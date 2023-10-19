using AutoMapper; 

namespace CarLinePickup.Domain.Mappings
{
    public class FamilyDataToFamilyDomain : Profile
    {
        public FamilyDataToFamilyDomain()
        {
            CreateMap<Data.Models.Family, Models.Family>()
                .ReverseMap()
                .ForMember(dest => dest.FamilyMembers, opts => opts.Ignore())
                .ForMember(dest => dest.Vehicles, opts => opts.Ignore());
        }
    }
}

