using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class AuthenticationUserDomainToAuthenticationUserResponse : Profile
    {
        public AuthenticationUserDomainToAuthenticationUserResponse()
        {
            CreateMap<Domain.Models.AuthenticationUser, AuthenticationUserResponse>();
        }
    }
}
