using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class GradeDomainToGradeResponse : Profile
    {
        public GradeDomainToGradeResponse()
        {
            CreateMap<Domain.Models.Grade, GradeResponse>();
        }
    }
}
