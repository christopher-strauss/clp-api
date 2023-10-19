using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class FamilyDomainToFamilyResponse : Profile
    {
        public FamilyDomainToFamilyResponse()
        {
            CreateMap<Domain.Models.Family, FamilyResponse>();
        }
    }
}
