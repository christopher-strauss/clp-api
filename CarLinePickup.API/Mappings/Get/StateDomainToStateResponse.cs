using AutoMapper;
using CarLinePickup.API.Models.Response;

namespace CarLinePickup.API.Mappings.Get
{
    public class StateDomainToStateResponse : Profile
    {
        public StateDomainToStateResponse()
        {
            CreateMap<Domain.Models.State, StateResponse>();
        }
    }
}
