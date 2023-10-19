using AutoMapper;

namespace CarLinePickup.Domain.Mappings
{
    public class AuthenticationUserProviderToAuthenticationUserProviderDomain : Profile
    {
        public AuthenticationUserProviderToAuthenticationUserProviderDomain()
        {
            CreateMap<Data.Models.AuthenticationUserProvider, Models.AuthenticationUserProvider>()
                .ReverseMap();
        }
    }
}

