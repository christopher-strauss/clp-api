using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class FamilyRegistrationDomainToFamilyRegistrationResponse : Profile
    {
        public FamilyRegistrationDomainToFamilyRegistrationResponse()
        {
            CreateMap<Domain.Models.FamilyRegistration, FamilyRegistrationResponse>();
        }
    }
}
