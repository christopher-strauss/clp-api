using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API.Mappings.Create
{
    public class EmployeeGradeRequestUpdateToGradeDomain : Profile
    {
        public EmployeeGradeRequestUpdateToGradeDomain()
        {
            CreateMap<EmployeeGradeRequestUpdate, Domain.Models.Grade>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.GradeId))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
