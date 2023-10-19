using AutoMapper;
using CarLinePickup.API.Models.Request.Update;

namespace CarLinePickup.API.Mappings.Update
{
    public class SchoolRequestUpdateToSchoolDomain : Profile
    {
        public SchoolRequestUpdateToSchoolDomain()
        {
            CreateMap<SchoolRequestUpdate, Domain.Models.School>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
