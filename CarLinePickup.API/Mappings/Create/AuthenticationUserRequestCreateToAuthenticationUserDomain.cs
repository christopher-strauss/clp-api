using AutoMapper;
using CarLinePickup.API.Models.Request.Create;
using CarLinePickup.Domain.Extensions;


namespace CarLinePickup.API.Mappings.Create
{
    public class AuthenticationUserRequestCreateToAuthenticationUserDomain : Profile
    {
        public AuthenticationUserRequestCreateToAuthenticationUserDomain()
        {
            CreateMap<AuthenticationUserRequestCreate, Domain.Models.AuthenticationUser>()
                .ReverseMap();
        }
    }
}
