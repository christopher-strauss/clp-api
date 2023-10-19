using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class SchoolDomainToSchoolResponse : Profile
    {
        public SchoolDomainToSchoolResponse()
        {
            CreateMap<Domain.Models.School, SchoolResponse>();
        }
    }
}
