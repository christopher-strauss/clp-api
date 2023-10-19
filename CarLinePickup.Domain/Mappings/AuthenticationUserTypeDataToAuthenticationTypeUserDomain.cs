using AutoMapper;

namespace CarLinePickup.Domain.Mappings
{
    public class AuthenticationUserTypeDataToAuthenticationTypeUserDomain : Profile
    {
        public AuthenticationUserTypeDataToAuthenticationTypeUserDomain()
        {
            CreateMap<Data.Models.AuthenticationUserType, Models.AuthenticationUserType>()
                .ReverseMap();
        }
    }
}