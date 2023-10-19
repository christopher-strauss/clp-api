using AutoMapper;

namespace CarLinePickup.Domain.Mappings
{
    public class AuthenticationUserDataToAuthenticationUserDomain : Profile
    {
        public AuthenticationUserDataToAuthenticationUserDomain()
        {
            CreateMap<Data.Models.AuthenticationUser, Models.AuthenticationUser>()
                .ForMember(dest => dest.AuthenticationUserType, opts => opts.MapFrom(src => src.AuthenticationUserType.Type))
                .ForMember(dest => dest.AuthenticationUserTypeId, opts => opts.MapFrom(src => src.AuthenticationUserType.Id))
                .ForMember(dest => dest.ProviderType, opts => opts.MapFrom(src => src.AuthenticationUserProvider.Name))
                .ForMember(dest => dest.ProviderTypeId, opts => opts.MapFrom(src => src.AuthenticationUserProvider.Id))
                .ReverseMap()
                .ForPath(dest => dest.AuthenticationUserType.Type, opts => opts.MapFrom(src => src.AuthenticationUserType))
                .ForPath(dest => dest.AuthenticationUserProvider.Name, opts => opts.MapFrom(src => src.ProviderType))
                .ForMember(dest => dest.AuthenticationUserProvider, opts => opts.Ignore())
                .ForMember(dest => dest.AuthenticationUserType, opts => opts.Ignore());

        }
    }
}

