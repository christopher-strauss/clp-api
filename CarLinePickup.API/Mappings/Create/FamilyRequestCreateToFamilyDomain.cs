using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class FamilyRequestCreateToFamilyDomain : Profile
    {
        public FamilyRequestCreateToFamilyDomain()
        {
            CreateMap<FamilyRequestCreate, Domain.Models.Family>()
                .ReverseMap();
        }
    }
}
