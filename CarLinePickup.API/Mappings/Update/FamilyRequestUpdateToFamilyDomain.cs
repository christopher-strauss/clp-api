using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API.Mappings.Update
{
    public class FamilyRequestUpdateToFamilyDomain : Profile
    {
        public FamilyRequestUpdateToFamilyDomain()
        {
            CreateMap<FamilyRequestUpdate, Domain.Models.Family>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
