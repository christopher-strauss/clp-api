using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class FamilyMemberDomainToFamilyMemberResponse : Profile
    {
        public FamilyMemberDomainToFamilyMemberResponse()
        {
            CreateMap<Domain.Models.FamilyMember, FamilyMemberResponse>();
        }
    }
}
