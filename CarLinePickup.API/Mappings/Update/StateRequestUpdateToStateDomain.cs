using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API.Mappings.Update
{
    public class StateRequestUpdateToStateDomain : Profile
    {
        public StateRequestUpdateToStateDomain()
        {
            CreateMap<StateRequestUpdate, Domain.Models.State>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));    
        }
    }
}
