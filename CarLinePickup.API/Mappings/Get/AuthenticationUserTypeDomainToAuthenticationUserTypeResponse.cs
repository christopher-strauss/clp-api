using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class AuthenticationUserTypeDomainToAuthenticationUserTypeResponse : Profile
    {
        public AuthenticationUserTypeDomainToAuthenticationUserTypeResponse()
        {
            CreateMap<Domain.Models.AuthenticationUserType, AuthenticationUserTypeResponse>();
        }
    }
}
