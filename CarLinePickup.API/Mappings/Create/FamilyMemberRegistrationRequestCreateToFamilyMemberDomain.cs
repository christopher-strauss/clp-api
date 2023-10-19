using AutoMapper;
using CarLinePickup.API.Models.Request.Create;

namespace CarLinePickup.API.Mappings.Create
{
    public class FamilyMemberRegistrationRequestCreateToFamilyMemberDomain : Profile
    {
        public FamilyMemberRegistrationRequestCreateToFamilyMemberDomain()
        {
            CreateMap<FamilyMemberRegistrationRequestCreate, Domain.Models.FamilyMember>()
                .ReverseMap();
        }
    }
}
