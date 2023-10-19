using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API.Mappings.Update
{
    public class CountyRequestUpdateToCountyDomain : Profile
    {
        public CountyRequestUpdateToCountyDomain()
        {
            CreateMap<CountyRequestUpdate, Domain.Models.County>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));    
        }
    }
}
