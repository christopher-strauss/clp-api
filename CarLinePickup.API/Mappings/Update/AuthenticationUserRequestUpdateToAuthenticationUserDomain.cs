using System;
using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API.Mappings.Create
{
    public class AuthenticationUserRequestUpdateToAuthenticationUserDomain : Profile
    {
        public AuthenticationUserRequestUpdateToAuthenticationUserDomain()
        {
            CreateMap<AuthenticationUserRequestUpdate, Domain.Models.AuthenticationUser>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember != null)
                    {
                        if (srcMember is Guid)
                        {
                            if (((Guid)srcMember) == Guid.Empty)
                            {
                                return false;
                            }
                        }

                        return true;
                    }

                    return false;
                }));
        }
    }
}
