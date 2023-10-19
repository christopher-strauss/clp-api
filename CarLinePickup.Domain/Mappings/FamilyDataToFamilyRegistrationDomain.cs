using AutoMapper; 

namespace CarLinePickup.Domain.Mappings
{
    public class FamilyDataToFamilyRegistrationDomain : Profile
    {
        public FamilyDataToFamilyRegistrationDomain()
        {
            CreateMap<Data.Models.Family, Models.FamilyRegistration>()
                .ReverseMap();
        }
    }
}

