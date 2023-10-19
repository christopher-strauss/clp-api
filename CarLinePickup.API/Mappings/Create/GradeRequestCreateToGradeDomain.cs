using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class GradeRequestCreateToGradeDomain : Profile
    {
        public GradeRequestCreateToGradeDomain()
        {
            CreateMap<GradeRequestCreate, Domain.Models.Grade>()
                .ReverseMap();
        }
    }
}
