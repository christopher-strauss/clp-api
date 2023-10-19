using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class FamilyMemberTypeDomainToFamilyMemberTypeResponse : Profile
    {
        public FamilyMemberTypeDomainToFamilyMemberTypeResponse()
        {
            CreateMap<Domain.Models.FamilyMemberType, FamilyMemberTypeResponse>();
        }
    }
}
