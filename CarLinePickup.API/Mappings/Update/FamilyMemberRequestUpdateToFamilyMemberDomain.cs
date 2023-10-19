using System;
using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API.Mappings.Update
{
    public class FamilyMemberRequestUpdateToFamilyMemberDomain : Profile
    {
        public FamilyMemberRequestUpdateToFamilyMemberDomain()
        {
            CreateMap<FamilyMemberRequestUpdate, Domain.Models.FamilyMember>()
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
