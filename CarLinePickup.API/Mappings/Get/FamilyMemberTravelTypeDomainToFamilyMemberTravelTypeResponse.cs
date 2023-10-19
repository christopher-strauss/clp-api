using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class FamilyMemberTravelTypeDomainToFamilyMemberTravelTypeResponse : Profile
    {
        public FamilyMemberTravelTypeDomainToFamilyMemberTravelTypeResponse()
        {
            CreateMap<Domain.Models.FamilyMemberTravelType, FamilyMemberTravelTypeResponse>();
        }
    }
}
