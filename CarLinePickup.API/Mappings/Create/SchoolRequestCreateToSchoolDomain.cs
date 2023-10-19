using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class SchoolRequestCreateToSchoolDomain : Profile
    {
        public SchoolRequestCreateToSchoolDomain()
        {
            CreateMap<SchoolRequestCreate, Domain.Models.School>()
                .ReverseMap();
        }
    }
}
