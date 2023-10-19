using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class CountyDomainToCountyResponse : Profile
    {
        public CountyDomainToCountyResponse()
        {
            CreateMap<Domain.Models.County, CountyResponse>();
        }
    }
}
