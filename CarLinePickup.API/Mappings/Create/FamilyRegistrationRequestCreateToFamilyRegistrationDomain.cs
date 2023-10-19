using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class FamilyRegistrationRequestCreateToFamilyRegistrationDomain : Profile
    {
        public FamilyRegistrationRequestCreateToFamilyRegistrationDomain()
        {
            CreateMap<FamilyRegistrationRequestCreate, Domain.Models.FamilyRegistration>()
                .ReverseMap();
        }
    }
}
