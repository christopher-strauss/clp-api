using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class EmployeeGradeRequestCreateToGradeDomain : Profile
    {
        public EmployeeGradeRequestCreateToGradeDomain()
        {
            CreateMap<EmployeeGradeRequestCreate, Domain.Models.Grade>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.GradeId))
                .ReverseMap();
        }
    }
}
