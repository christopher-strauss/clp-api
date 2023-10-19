using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API.Mappings.Update
{
    public class GradeRequestUpdateToGradeDomain : Profile
    {
        public GradeRequestUpdateToGradeDomain()
        {
            CreateMap<GradeRequestUpdate, Domain.Models.Grade>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
