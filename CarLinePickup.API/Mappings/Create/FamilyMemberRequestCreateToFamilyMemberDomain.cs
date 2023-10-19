using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class FamilyMemberRequestCreateToFamilyMemberDomain : Profile
    {
        public FamilyMemberRequestCreateToFamilyMemberDomain()
        {
            CreateMap<FamilyMemberRequestCreate, Domain.Models.FamilyMember>()
                .ReverseMap();
        }
    }
}
